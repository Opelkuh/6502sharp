using System.Drawing;

namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        private int Frame;
        private int Scanline;
        private int Cycles;

        #region Render registers
        private byte nametableRaw;
        private int backgroundPalette;
        private byte backgroundLo;
        private byte backgroundHi;

        private Color[] backgroundData;
        private Sprite?[] scanlineSprites;
        #endregion

        private int nmiDelay;
        private bool nmiPrevious;
        private bool nmiOccured;

        private void nmiChange()
        {
            bool temp = status.VBlank && ctrl.NMIEnabled;
            if (temp && !nmiPrevious)
            {
                nmiDelay = 15;
            }
            nmiPrevious = temp;
        }

        public void NextTick()
        {
            if (nmiDelay > 0)
            {
                nmiDelay--;

                if (nmiDelay == 0 && ctrl.NMIEnabled && status.VBlank)
                {
                    mach.CPU.InterruptNMI();
                }
            }


            if (Scanline >= 261)
            {
                prerenderScanline();
                renderScanline(false);
            }
            else if (Range.Fits(0, 239, Scanline)) renderScanline(true);
            else if (Range.Fits(241, 260, Scanline)) vblankScanline();

            // fetch sprites
            if (mask.RenderingEnabled)
            {
                scanlineSprites = prepareSprites();
            }

            // increment cycles
            Cycles++;
            if (Scanline >= 261 && (Frame & 1) == 0)
            {
                if (Cycles < 340) return;
            }
            else
            {
                if (Cycles < 341) return;
            }

            // increment scanline
            Cycles = 0;
            Scanline++;

            if (Scanline > 261) Scanline = 0;
        }

        private void prerenderScanline()
        {
            if (Cycles == 1)
            {
                status.VBlank = false;
                status.Sprite0Hit = false;
                status.SpriteOverflow = false;
                nmiChange();
            }

            if (mask.RenderingEnabled && Range.Fits(280, 304, Cycles))
            {
                VRAMAddress.Value &= ~0x7BE0;
                VRAMAddress.Value |= TempVRAMAddress.Value & 0x7BE0;
            }
        }

        private void renderScanline(bool draw)
        {
            if (!mask.RenderingEnabled) return;
            if (Cycles == 0) return;

            if (draw && Cycles <= 256) nextPixel();
            loadRenderRegisters();

            // increment x
            if ((Cycles >= 328 || Cycles <= 256) && (Cycles % 8) == 0)
            {
                incrementX();
            }
            // increment y
            if (Cycles == 256) incrementY();
            // copy horizontal pos from tempVram to vram
            if (Cycles == 257)
            {
                VRAMAddress.Value &= ~0x041f;
                VRAMAddress.Value |= TempVRAMAddress.Value & 0x041f;
            }
        }

        private void vblankScanline()
        {
            if (Scanline != 241 | Cycles != 1) return;

            status.VBlank = true;
            nmiChange();
        }

        private void loadRenderRegisters()
        {
            if (!Range.Fits(1, 256, Cycles) && !Range.Fits(321, 336, Cycles)) return;

            int op = (Cycles - 1) % 8;

            switch (op)
            {
                case 0:
                    nametableRaw = vram.Get(VRAMAddress.TileAddress);
                    break;
                case 2:
                    backgroundPalette = getBackgroundPalette();
                    break;
                case 4:
                    backgroundLo = vram.Get(getBackgroundPatternAddress());
                    break;
                case 6:
                    backgroundLo = vram.Get(getBackgroundPatternAddress() + 8);
                    backgroundData = fetchBackgroundData();
                    break;
            }
        }
    }
}