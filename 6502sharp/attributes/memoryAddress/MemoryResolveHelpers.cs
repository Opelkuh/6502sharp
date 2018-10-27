namespace _6502sharp
{
    internal static class MemoryResolveHelpers
    {
        public static int ZeroPageIndexed(ICpu cpu, ref byte[] raw, in byte addition)
        {
            int target = raw[0] + addition;
            return target % 256;
        }

        public static int AbsoluteIndexed(ICpu cpu, ref byte[] raw, in byte addition)
        {
            // check for carry 1 cycle penalty
            int lower = raw[0] + addition;
            if (lower > 255) cpu.SleepCycles++;

            return LEHelper.From(raw) + addition;
        }
    }
}
