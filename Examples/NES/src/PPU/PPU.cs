namespace NES.PPU
{
    class PPU
    {
        private NESMachine mach;

        private OAMMemory OAM;

        public PPU(NESMachine machine)
        {
            mach = machine;
        }
    }
}
