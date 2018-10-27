namespace _6502sharp
{
    public class IndirectYAddressAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            int memAddr = raw[0];
            int memAddr2 = (memAddr + 1) % 256;

            byte[] target = { cpu.Memory.Get(memAddr), cpu.Memory.Get(memAddr2) };

            // carry 1 cycle penalty
            int lower = target[0] + cpu.Y.Value;
            if (lower > 255) cpu.SleepCycles++;

            int address = LEHelper.From(target);
            return address + cpu.Y.Value;
        }
    }
}