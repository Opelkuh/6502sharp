namespace NES.PPU
{
    class PictureProcessingUnit
    {
        public NESMachine Machine => mach;
        public PPUMemory VRAM => vram;
        public OAMMemory OAM => oam;

        private NESMachine mach;
        private PPUMemory vram;
        private OAMMemory oam;

        public PictureProcessingUnit(NESMachine machine)
        {
            mach = machine;
        }
    }
}
