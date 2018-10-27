namespace _6502sharp
{
    public class AbsoluteAddressXAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            int first = raw[0] + cpu.X.Value;

            if (first > 255) cpu.SleepCycles++;

            return first | (raw[1] << 8);
        }
    }
}
