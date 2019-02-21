namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        private void startDMA(byte hiByte)
        {
            // add sleep cycles to CPU
            mach.CPU.SleepCycles += 513;

            // add extra cycle if FinishedCycles is odd after transfer
            int total = mach.CPU.FinishedCycles + mach.CPU.SleepCycles;
            if ((total & 1) == 0) mach.CPU.SleepCycles++;

            // DMA transfer
            int start = hiByte << 8;
            int end = start | 0x00FF;
            for (int i = start; i <= end; i++)
            {
                byte val = mach.Memory.Get(i);
                OAM.Set(OAMAddress++, val);
            }
        }
    }
}