using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class ORA
    {
        private ICpu _cpu;

        public ORA(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xA9, 2)]
        public void ORA_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0x05, 3), ZeroPage]
        [CPUInstruction(0x15, 4), ZeroPageX]
        [CPUInstruction(0x0D, 4), AbsoluteAddress]
        [CPUInstruction(0x1D, 4), AbsoluteAddressX]
        [CPUInstruction(0x19, 4), AbsoluteAddressY]
        [CPUInstruction(0x01, 6), IndirectXAddress]
        [CPUInstruction(0x11, 5), IndirectYAddress]
        [CPUInstruction(0x12, 6, CPUType.CMOS), IndirectAddress]
        public void ORA_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            _cpu.A.Value |= value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.A.Value);
        }
    }
}