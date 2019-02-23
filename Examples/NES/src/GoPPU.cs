/*
Ported from Go to C#

Original: https://github.com/fogleman/nes/blob/master/nes/ppu.go

------------------------------------------------------------------------

Copyright (C) 2015 Michael Fogleman

Permission is hereby granted, free of charge, to any person obtaining a copy of this software
and associated documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

 */

using System;
using System.Drawing;
using NES.PPU;
using NES.Rendering;

namespace NES
{
    class GoPPU
    {
        public PPUMemory Memory;
        public NESMachine Machine;

        int Cycle;
        int Scanline;
        long Frame;

        // storage variables
        byte[] paletteData = new byte[32];
        byte[] nameTableData = new byte[2048];
        byte[] oamData = new byte[256];

        public Bitmap front;
        public Bitmap back;

        // PPU registers
        int v; // current vram address (15 bit)
        int t; // temporary vram address (15 bit)
        byte x;   // fine x scroll (3 bit)
        byte w;   // write toggle (1 bit)
        byte f;   // even/odd frame flag (1 bit)

        byte register;

        // NMI flags
        bool nmiOccurred;
        bool nmiOutput;
        bool nmiPrevious;
        byte nmiDelay;

        // background temporary variables
        byte nameTableByte;
        byte attributeTableByte;
        byte lowTileByte;
        byte highTileByte;
        long tileData;

        // sprite temporary variables
        int spriteCount;
        uint[] spritePatterns = new uint[8];
        byte[] spritePositions = new byte[8];
        byte[] spritePriorities = new byte[8];
        byte[] spriteIndexes = new byte[8];

        // $2000 PPUCTRL
        byte flagNameTable;
        byte flagIncrement;
        byte flagSpriteTable;
        byte flagBackgroundTable;
        byte flagSpriteSize;
        byte flagMasterSlave;

        // $2001 PPUMASK
        byte flagGrayscale; // 0: color; 1: grayscale
        byte flagShowLeftBackground; // 0: hide; 1: show
        byte flagShowLeftSprites; // 0: hide; 1: show
        byte flagShowBackground; // 0: hide; 1: show
        byte flagShowSprites; // 0: hide; 1: show
        byte flagRedTint; // 0: normal; 1: emphasized
        byte flagGreenTint; // 0: normal; 1: emphasized
        byte flagBlueTint; // 0: normal; 1: emphasized

        // $2002 PPUSTATUS
        byte flagSpriteZeroHit;
        byte flagSpriteOverflow;

        // $2003 OAMADDR
        byte oamAddress;

        // $2007 PPUDATA
        byte bufferedData; // for buffered reads

        public GoPPU(NESMachine machine)
        {
            Machine = machine;
            Memory = new PPUMemory();

            front = new Bitmap(256, 240);
            back = new Bitmap(256, 240);
            Reset();
        }


        public void Reset()
        {
            Cycle = 340;
            Scanline = 240;
            Frame = 0;
            writeControl(0);
            writeMask(0);
            writeOAMAddress(0);
        }

        private byte readPalette(short address)
        {
            if (address >= 16 && address % 4 == 0)
            {
                address -= 16;
            }
            return paletteData[address];
        }

        private void writePalette(short address, byte value)
        {
            if (address >= 16 && address % 4 == 0)
            {
                address -= 16;
            }
            paletteData[address] = value;
        }

        public byte readRegister(int address)
        {
            switch (address)
            {
                case 0x2002:
                    return readStatus();
                case 0x2004:
                    return readOAMData();
                case 0x2007:
                    return readData();
            }
            return 0;
        }

        public void writeRegister(int address, byte value)
        {
            register = value;
            switch (address)
            {
                case 0x2000:
                    writeControl(value); break;
                case 0x2001:
                    writeMask(value); break;
                case 0x2003:
                    writeOAMAddress(value); break;
                case 0x2004:
                    writeOAMData(value); break;
                case 0x2005:
                    writeScroll(value); break;
                case 0x2006:
                    writeAddress(value); break;
                case 0x2007:
                    writeData(value); break;
                case 0x4014:
                    writeDMA(value); break;
            }
        }

        // $2000: PPUCTRL
        private void writeControl(byte value)
        {
            flagNameTable = (byte)((value >> 0) & 3);
            flagIncrement = (byte)((value >> 2) & 1);
            flagSpriteTable = (byte)((value >> 3) & 1);
            flagBackgroundTable = (byte)((value >> 4) & 1);
            flagSpriteSize = (byte)((value >> 5) & 1);
            flagMasterSlave = (byte)((value >> 6) & 1);
            nmiOutput = ((value >> 7) & 1) == 1;
            nmiChange();
            // t: ....BA.. ........ = d: ......BA
            t = (t & 0xF3FF) | ((value & 0x03) << 10);
        }

        // $2001: PPUMASK
        private void writeMask(byte value)
        {
            flagGrayscale = (byte)((value >> 0) & 1);
            flagShowLeftBackground = (byte)((value >> 1) & 1);
            flagShowLeftSprites = (byte)((value >> 2) & 1);
            flagShowBackground = (byte)((value >> 3) & 1);
            flagShowSprites = (byte)((value >> 4) & 1);
            flagRedTint = (byte)((value >> 5) & 1);
            flagGreenTint = (byte)((value >> 6) & 1);
            flagBlueTint = (byte)((value >> 7) & 1);
        }

        // $2002: PPUSTATUS
        private byte readStatus()
        {
            int result = register & 0x1F;
            result |= flagSpriteOverflow << 5;
            result |= flagSpriteZeroHit << 6;
            if (nmiOccurred)
            {
                result |= 1 << 7;
            }
            nmiOccurred = false;
            nmiChange();
            // w:                   = 0
            w = 0;
            return (byte)result;
        }

        // $2003: OAMADDR
        private void writeOAMAddress(byte value)
        {
            oamAddress = value;
        }

        // $2004: OAMDATA (read)
        private byte readOAMData()
        {
            return oamData[oamAddress];
        }

        // $2004: OAMDATA (write)
        private void writeOAMData(byte value)
        {
            oamData[oamAddress] = value;
            oamAddress++;
        }

        // $2005: PPUSCROLL
        private void writeScroll(byte value)
        {
            if (w == 0)
            {
                // t: ........ ...HGFED = d: HGFED...
                // x:               CBA = d: .....CBA
                // w:                   = 1
                t = (t & 0xFFE0) | ((value) >> 3);
                x = (byte)(value & 0x07);
                w = 1;
            }
            else
            {
                // t: .CBA..HG FED..... = d: HGFEDCBA
                // w:                   = 0
                t = (t & 0x8FFF) | ((value & 0x07) << 12);
                t = (t & 0xFC1F) | ((value & 0xF8) << 2);
                w = 0;
            }
        }

        // $2006: PPUADDR
        private void writeAddress(byte value)
        {
            if (w == 0)
            {
                // t: ..FEDCBA ........ = d: ..FEDCBA
                // t: .X...... ........ = 0
                // w:                   = 1
                t = (t & 0x80FF) | (((value) & 0x3F) << 8);
                w = 1;
            }
            else
            {
                // t: ........ HGFEDCBA = d: HGFEDCBA
                // v                    = t
                // w:                   = 0
                t = (t & 0xFF00) | (value);
                v = t;
                w = 0;
            }
        }

        // $2007: PPUDATA (read)
        private byte readData()
        {
            byte value = Memory.Get(v);
            // emulate buffered reads
            if (v % 0x4000 < 0x3F00)
            {
                byte buffered = bufferedData;
                bufferedData = value;
                value = buffered;
            }
            else
            {
                bufferedData = Memory.Get(v - 0x1000);
            }
            // increment address
            if (flagIncrement == 0)
            {
                v += 1;
            }
            else
            {
                v += 32;
            }
            return value;
        }

        // $2007: PPUDATA (write)
        private void writeData(byte value)
        {
            Memory.Set(v, value);
            if (flagIncrement == 0)
            {
                v += 1;
            }
            else
            {
                v += 32;
            }
        }

        // $4014: OAMDMA
        private void writeDMA(byte value)
        {
            int address = value << 8;
            for (int i = 0; i < 256; i++)
            {
                oamData[oamAddress] = Machine.CPU.Memory.Get(address);
                oamAddress++;
                address++;
            }
            Machine.CPU.SleepCycles += 513;
            if (Machine.CPU.FinishedCycles % 2 == 1)
            {
                Machine.CPU.SleepCycles++;
            }
        }

        // NTSC Timing Helper Functions

        private void incrementX()
        {
            // increment hori(v)
            // if coarse X == 31
            if ((v & 0x001F) == 31)
            {
                // coarse X = 0
                v &= 0xFFE0;
                // switch horizontal nametable
                v ^= 0x0400;
            }
            else
            {
                // increment coarse X
                v++;
            }
        }

        private void incrementY()
        {
            // increment vert(v)
            // if fine Y < 7
            if ((v & 0x7000) != 0x7000)
            {
                // increment fine Y
                v += 0x1000;
            }
            else
            {
                // fine Y = 0
                v &= 0x8FFF;
                // let y = coarse Y
                int y = (v & 0x03E0) >> 5;
                if (y == 29)
                {
                    // coarse Y = 0
                    y = 0;
                    // switch vertical nametable
                    v ^= 0x0800;
                }
                else if (y == 31)
                {
                    // coarse Y = 0, nametable not switched
                    y = 0;
                }
                else
                {
                    // increment coarse Y
                    y++;
                }
                // put coarse Y back into v
                v = (v & 0xFC1F) | (y << 5);
            }
        }

        private void copyX()
        {
            // hori(v) = hori(t)
            // v: .....F.. ...EDCBA = t: .....F.. ...EDCBA
            v = (v & 0xFBE0) | (t & 0x041F);
        }

        private void copyY()
        {
            // vert(v) = vert(t)
            // v: .IHGF.ED CBA..... = t: .IHGF.ED CBA.....
            v = (v & 0x841F) | (t & 0x7BE0);
        }

        private void nmiChange()
        {
            bool nmi = nmiOutput && nmiOccurred;
            if (nmi && !nmiPrevious)
            {
                // TODO: this fixes some games but the delay shouldn't have to be so
                // long, so the timings are off somewhere
                nmiDelay = 15;
            }
            nmiPrevious = nmi;
        }

        private void setVerticalBlank()
        {
            Bitmap temp = front;

            front = back;
            back = temp;

            nmiOccurred = true;
            nmiChange();
        }

        private void clearVerticalBlank()
        {
            nmiOccurred = false;
            nmiChange();
        }

        private void fetchNameTableByte()
        {
            int locV = v;
            int address = 0x2000 | (locV & 0x0FFF);
            nameTableByte = Memory.Get(address);
        }

        private void fetchAttributeTableByte()
        {
            int locV = v;
            int address = 0x23C0 | (locV & 0x0C00) | ((locV >> 4) & 0x38) | ((locV >> 2) & 0x07);
            int shift = ((locV >> 4) & 4) | (locV & 2);
            attributeTableByte = (byte)(((Memory.Get(address) >> shift) & 3) << 2);
        }

        private void fetchLowTileByte()
        {
            int fineY = (v >> 12) & 7;
            byte table = flagBackgroundTable;
            byte tile = nameTableByte;
            int address = (0x1000 * table) + (tile * 16) + fineY;
            lowTileByte = Memory.Get(address);
        }

        private void fetchHighTileByte()
        {
            int fineY = (v >> 12) & 7;
            byte table = flagBackgroundTable;
            byte tile = nameTableByte;
            int address = (0x1000 * table) + (tile * 16) + fineY;
            highTileByte = Memory.Get(address + 8);
        }

        private void storeTileData()
        {
            uint data = 0;
            for (int i = 0; i < 8; i++)
            {
                byte a = attributeTableByte;
                int p1 = (lowTileByte & 0x80) >> 7;
                int p2 = (highTileByte & 0x80) >> 6;
                lowTileByte <<= 1;
                highTileByte <<= 1;
                data <<= 4;
                data |= (uint)(a | p1 | p2);
            }
            tileData |= data;
        }

        private uint fetchTileData()
        {
            return (uint)(tileData >> 32);
        }

        private byte backgroundPixel()
        {
            if (flagShowBackground == 0)
            {
                return 0;
            }
            uint data = fetchTileData() >> ((7 - x) * 4);
            return (byte)(data & 0x0F);
        }

        Tuple<byte, byte> spritePixel()
        {
            if (flagShowSprites == 0)
            {
                return new Tuple<byte, byte>(0, 0);
            }
            for (int i = 0; i < spriteCount; i++)
            {
                int offset = (Cycle - 1) - spritePositions[i];
                if (offset < 0 || offset > 7)
                {
                    continue;
                }
                offset = 7 - offset;
                byte color = (byte)((spritePatterns[i] >> (byte)(offset * 4)) & 0x0F);
                if (color % 4 == 0)
                {
                    continue;
                }
                return new Tuple<byte, byte>((byte)i, color);
            }
            return new Tuple<byte, byte>(0, 0);
        }

        private void renderPixel()
        {
            int x = Cycle - 1;
            int y = Scanline;
            byte background = backgroundPixel();
            Tuple<byte, byte> spritePix = spritePixel();
            byte i = spritePix.Item1;
            byte sprite = spritePix.Item2;
            if (x < 8 && flagShowLeftBackground == 0)
            {
                background = 0;
            }
            if (x < 8 && flagShowLeftSprites == 0)
            {
                sprite = 0;
            }
            bool b = (background % 4) != 0;
            bool s = (sprite % 4) != 0;
            byte color;
            if (!b && !s)
            {
                color = 0;
            }
            else if (!b && s)
            {
                color = (byte)(sprite | 0x10);
            }
            else if (b && !s)
            {
                color = background;
            }
            else
            {
                if (spriteIndexes[i] == 0 && x < 255)
                {
                    flagSpriteZeroHit = 1;
                }
                if (spritePriorities[i] == 0)
                {
                    color = (byte)(sprite | 0x10);
                }
                else
                {
                    color = background;
                }
            }
            Color toDraw = ColorPalette.Get(color % 64);
            lock(back) back.SetPixelGL(x, y, toDraw);
        }

        uint fetchSpritePattern(int i, int row)
        {
            byte tile = oamData[i * 4 + 1];
            byte attributes = oamData[i * 4 + 2];
            int address;
            if (flagSpriteSize == 0)
            {
                if ((attributes & 0x80) == 0x80)
                {
                    row = 7 - row;
                }
                byte table = flagSpriteTable;
                address = (0x1000 * table) + (tile * 16) + (row);
            }
            else
            {
                if ((attributes & 0x80) == 0x80)
                {
                    row = 15 - row;
                }
                byte table = (byte)(tile & 1);
                tile &= 0xFE;
                if (row > 7)
                {
                    tile++;
                    row -= 8;
                }
                address = (0x1000 * table) + (tile * 16) + row;
            }
            int a = (attributes & 3) << 2;
            byte lowTileByte = Memory.Get(address);
            byte highTileByte = Memory.Get(address + 8);
            uint data = 0;
            for (int x = 0; x < 8; x++)
            {
                byte p1, p2;
                if ((attributes & 0x40) == 0x40)
                {
                    p1 = (byte)((lowTileByte & 1) << 0);
                    p2 = (byte)((highTileByte & 1) << 1);
                    lowTileByte >>= 1;
                    highTileByte >>= 1;
                }
                else
                {
                    p1 = (byte)((lowTileByte & 0x80) >> 7);
                    p2 = (byte)((highTileByte & 0x80) >> 6);
                    lowTileByte <<= 1;
                    highTileByte <<= 1;
                }
                data <<= 4;
                data |= (uint)(a | p1 | p2);
            }
            return data;
        }

        private void evaluateSprites()
        {
            int h;
            if (flagSpriteSize == 0)
            {
                h = 8;
            }
            else
            {
                h = 16;
            }
            int count = 0;
            for (int i = 0; i < 64; i++)
            {
                byte y = oamData[i * 4 + 0];
                byte a = oamData[i * 4 + 2];
                byte x = oamData[i * 4 + 3];
                int row = Scanline - y;
                if (row < 0 || row >= h)
                {
                    continue;
                }
                if (count < 8)
                {
                    spritePatterns[count] = fetchSpritePattern(i, row);
                    spritePositions[count] = x;
                    spritePriorities[count] = (byte)((a >> 5) & 1);
                    spriteIndexes[count] = (byte)i;
                }
                count++;
            }
            if (count > 8)
            {
                count = 8;
                flagSpriteOverflow = 1;
            }
            spriteCount = count;
        }

        // tick updates Cycle, ScanLine and Frame counters
        private void tick()
        {
            if (nmiDelay > 0)
            {
                nmiDelay--;
                if (nmiDelay == 0 && nmiOutput && nmiOccurred)
                {
                    Machine.CPU.InterruptNMI();
                }
            }

            if (flagShowBackground != 0 || flagShowSprites != 0)
            {
                if (f == 1 && Scanline == 261 && Cycle == 339)
                {
                    Cycle = 0;
                    Scanline = 0;
                    Frame++;
                    f ^= 1;
                    return;
                }
            }
            Cycle++;
            if (Cycle > 340)
            {
                Cycle = 0;
                Scanline++;
                if (Scanline > 261)
                {
                    Scanline = 0;
                    Frame++;
                    f ^= 1;
                }
            }
        }

        // Step executes a single PPU cycle
        public void NextTick()
        {
            tick();

            bool renderingEnabled = flagShowBackground != 0 || flagShowSprites != 0;
            bool preLine = Scanline == 261;
            bool visibleLine = Scanline < 240;
            // postLine := ppu.ScanLine == 240
            bool renderLine = preLine || visibleLine;
            bool preFetchCycle = Cycle >= 321 && Cycle <= 336;
            bool visibleCycle = Cycle >= 1 && Cycle <= 256;
            bool fetchCycle = preFetchCycle || visibleCycle;

            // background logic
            if (renderingEnabled)
            {
                if (visibleLine && visibleCycle)
                {
                    renderPixel();
                }
                if (renderLine && fetchCycle)
                {
                    tileData <<= 4;
                    switch (Cycle % 8)
                    {
                        case 1:
                            fetchNameTableByte(); break;
                        case 3:
                            fetchAttributeTableByte(); break;
                        case 5:
                            fetchLowTileByte(); break;
                        case 7:
                            fetchHighTileByte(); break;
                        case 0:
                            storeTileData(); break;
                    }
                }
                if (preLine && Cycle >= 280 && Cycle <= 304)
                {
                    copyY();
                }
                if (renderLine)
                {
                    if (fetchCycle && Cycle % 8 == 0)
                    {
                        incrementX();
                    }
                    if (Cycle == 256)
                    {
                        incrementY();
                    }
                    if (Cycle == 257)
                    {
                        copyX();
                    }
                }
            }

            // sprite logic
            if (renderingEnabled)
            {
                if (Cycle == 257)
                {
                    if (visibleLine)
                    {
                        evaluateSprites();
                    }
                    else
                    {
                        spriteCount = 0;
                    }
                }
            }

            // vblank logic
            if (Scanline == 241 && Cycle == 1)
            {
                setVerticalBlank();
            }
            if (preLine && Cycle == 1)
            {
                clearVerticalBlank();
                flagSpriteZeroHit = 0;
                flagSpriteOverflow = 0;
            }
        }
    }
}