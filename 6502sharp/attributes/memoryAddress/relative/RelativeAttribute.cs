namespace _6502sharp
{
    public class RelativeAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] rawAddress)
        {
            sbyte offset = (sbyte)rawAddress[0];

            int address = cpu.PC.Value + offset;

            if (cpu.Type == CPUType.CMOS || (address & 0xFF00) != (cpu.PC.Value & 0xFF00)) cpu.SleepCycles++;

            return address;
        }
    }
}
