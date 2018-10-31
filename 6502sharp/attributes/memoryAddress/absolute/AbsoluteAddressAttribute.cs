using _6502sharp.Helpers;

namespace _6502sharp
{
    public class AbsoluteAddressAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            return LEHelper.From(raw);
        }
    }
}
