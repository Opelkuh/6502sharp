namespace _6502sharp.Reflection
{
    public class ZeroPageAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            return raw[0];
        }
    }
}
