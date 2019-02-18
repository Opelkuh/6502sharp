namespace NES.Mappers
{
    class NROM : IMapper
    {
        private NESMachine machine;
        private RomData rom;
        private GetPRGRom getAction;

        private static Range PRGRamRange = new Range(0x8000, 0xBFFF);
        private static Mirror PRGRamMirror = new Mirror(PRGRamRange, new Range(0xC000, 0xFFFF));

        private delegate byte? GetPRGRom(int address);

        public NROM(NESMachine machine, RomData rom)
        {
            this.machine = machine;
            this.rom = rom;

            if (rom.PRGRom.Length <= 16384) getAction = get16K;
            else getAction = get32K;
        }

        public byte? Get(int address)
        {
            if (!PRGRamRange.Fits(address)) return null;

            return getAction(address);
        }

        public bool Set(int address, byte value)
        {
            return false;
        }

        public void Dispose()
        {
            return;
        }

        private byte? get16K(int address)
        {
            PRGRamMirror.Transform(ref address);

            return rom.PRGRom[address - 0x8000];
        }

        private byte? get32K(int address)
        {
            return rom.PRGRom[address - 0x8000];
        }
    }
}