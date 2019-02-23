namespace NES.Mappers
{
    class NROM : IMapper
    {
        private NESMachine machine;
        private RomData rom;
        private GetPRGRom getAction;

        private static Range PRGRomRange = new Range(0x8000, 0xBFFF);
        private static Mirror PRGRomMirror = new Mirror(PRGRomRange, new Range(0xC000, 0xFFFF));

        private delegate byte? GetPRGRom(int address);

        public NROM(NESMachine machine, RomData rom)
        {
            this.machine = machine;
            this.rom = rom;

            for (int i = 0x8000; i <= 0xFFFF; i++)
            {
                byte data = rom.PRGRom[(i - 0x8000) % (rom.PRGRom.Length)];

                machine.Memory.Set(i, data);
            }

            for (int i = 0; i <= 0x1FFF; i++)
            {
                byte data = rom.CHRRom[i % rom.CHRRom.Length];

                machine.PPU.Memory.Set(i, data);
            }
        }

        public byte? Get(int address)
        {
            return null;
        }

        public bool Set(int address, byte value)
        {
            return false;
        }

        public void Dispose()
        {
            return;
        }
    }
}