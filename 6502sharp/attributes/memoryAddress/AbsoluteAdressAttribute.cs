using System;

namespace _6502sharp
{
    public class AbsoluteAdressAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(byte[] raw)
        {
            return (raw[0] * 1000) + raw[1];
        }
    }
}
