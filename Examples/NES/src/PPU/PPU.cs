namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        public NESMachine Machine => mach;
        public PPUMemory VRAM => vram;
        public OAMMemory OAM => oam;

        private NESMachine mach;
        private PPUMemory vram = new PPUMemory();
        private OAMMemory oam = new OAMMemory();

        public PictureProcessingUnit(NESMachine machine)
        {
            mach = machine;
        }
    }
}
