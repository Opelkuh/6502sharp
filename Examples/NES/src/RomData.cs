namespace NES
{
    struct RomData
    {
        // TYPES
        public RomType Type;
        public MachineType Machine;
        public TVSystem TVSystem;
        public ControllerType DefaultController;

        // ROM AND RAM
        public byte[] Trainer;
        public byte[] PRGRom;
        public byte[] CHRRom;
        public int PRGRomSize;
        public int CHRRomSize;

        public int PRGRamSize;
        public int PRGNVRamSize;
        public int CHRRamSize;
        public int CHRNVRamSize;

        // MAPPERS
        public int MapperNum;
        public int SubmapperNum;
        public Mirroring PPUMirroring;

        // FLAGS
        public bool HasBattery;
        public bool HasTrainer;
    }
}
