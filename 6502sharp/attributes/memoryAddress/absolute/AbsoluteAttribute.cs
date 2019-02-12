using _6502sharp.Helpers;

namespace _6502sharp.Reflection
{
    public class AbsoluteAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            return LEHelper.From(raw);
        }
    }
}
