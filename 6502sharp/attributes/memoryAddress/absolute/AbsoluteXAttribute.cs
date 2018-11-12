namespace _6502sharp
{
    public class AbsoluteXAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            return MemoryResolveHelpers.AbsoluteIndexed(cpu, ref raw, cpu.X.Value);
        }
    }
}
