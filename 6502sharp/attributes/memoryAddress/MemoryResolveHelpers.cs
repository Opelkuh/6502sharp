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
            int first = raw[0] + addition;

            if (first > 255) cpu.SleepCycles++;

            return first | (raw[1] << 8);
        }
    }
}
