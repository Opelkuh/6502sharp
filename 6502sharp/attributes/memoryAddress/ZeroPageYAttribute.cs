namespace _6502sharp
{
    public class ZeroPageYAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            int target = raw[0] + cpu.Y.Value;

            if(target > 256) cpu.SleepCycles++;

            return target % 256;
        }
    }
}
