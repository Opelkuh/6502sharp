namespace _6502sharp
{
    public class AbsoluteAddressYAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            int first = raw[0] + cpu.Y.Value;

            if (first > 255) cpu.SleepCycles++;

            return first | (raw[1] << 8);
        }
    }
}
