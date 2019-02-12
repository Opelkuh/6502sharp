using _6502sharp.Helpers;

namespace _6502sharp.Reflection
{
    public class IndirectAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            if (cpu.Type == CPUType.CMOS) return CMOSResolve(cpu, ref raw);
            else return NMOSResolve(cpu, ref raw);
        }

        private int NMOSResolve(ICpu cpu, ref byte[] raw)
        {
            int firstLoc = LEHelper.From(raw);

            // 6502 bug (https://everything2.com/node/868510)
            raw[0] = (byte)((raw[0] + 1) % 256);

            int secondLoc = LEHelper.From(raw);

            byte[] target = { cpu.Memory.Get(firstLoc), cpu.Memory.Get(secondLoc) };

            return LEHelper.From(target);
        }

        private int CMOSResolve(ICpu cpu, ref byte[] raw)
        {
            int loc = LEHelper.From(raw);

            byte[] target = new byte[] { cpu.Memory.Get(loc), cpu.Memory.Get(loc + 1) };

            return LEHelper.From(target);
        }
    }
}
