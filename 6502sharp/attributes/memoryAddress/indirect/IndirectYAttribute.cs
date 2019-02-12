using _6502sharp.Helpers;

namespace _6502sharp.Reflection
{
    public class IndirectYAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            int memAddr = raw[0];
            int memAddr2 = (memAddr + 1) % 256;

            byte[] target = { cpu.Memory.Get(memAddr), cpu.Memory.Get(memAddr2) };

            // carry 1 cycle penalty
            int lower = target[0] + cpu.Y.Value;
            if (cpu.Type == CPUType.CMOS || lower > 0xFF) cpu.SleepCycles++;

            int address = LEHelper.From(target);
            return address + cpu.Y.Value;
        }
    }
}
