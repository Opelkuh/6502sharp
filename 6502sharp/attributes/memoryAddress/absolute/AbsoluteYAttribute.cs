namespace _6502sharp.Reflection
{
    public class AbsoluteYAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            return MemoryResolveHelpers.AbsoluteIndexed(cpu, ref raw, cpu.Y.Value);
        }
    }
}
