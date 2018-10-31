using _6502sharp.Helpers;

namespace _6502sharp
{
    public class IndirectXAddressAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            int memAddr = (raw[0] + cpu.X.Value) % 256;
            int memAddr2 = (memAddr + 1) % 256;

            byte[] target = { cpu.Memory.Get(memAddr), cpu.Memory.Get(memAddr2) };

            return LEHelper.From(target);
        }
    }
}
