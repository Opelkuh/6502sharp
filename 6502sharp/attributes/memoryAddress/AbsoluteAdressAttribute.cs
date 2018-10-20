using System;

namespace _6502sharp
{
    public class AbsoluteAdressAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(byte[] raw)
        {
            throw new NotImplementedException();
        }
    }
}
