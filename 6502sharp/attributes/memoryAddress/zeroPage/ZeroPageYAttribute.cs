namespace _6502sharp.Reflection
{
    public class ZeroPageYAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            return MemoryResolveHelpers.ZeroPageIndexed(cpu, ref raw, cpu.Y.Value);
        }
    }
}
