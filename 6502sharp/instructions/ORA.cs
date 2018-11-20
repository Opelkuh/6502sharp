using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ORA
    {
        private ICpu _cpu;

        public ORA(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x09, 2)]
        public void ORA_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0x05, 3), ZeroPage]
        [CPUInstruction(0x15, 4), ZeroPageX]
        [CPUInstruction(0x0D, 4), Absolute]
        [CPUInstruction(0x1D, 4), AbsoluteX]
        [CPUInstruction(0x19, 4), AbsoluteY]
        [CPUInstruction(0x01, 6), IndirectX]
        [CPUInstruction(0x11, 5), IndirectY]
        [CPUInstruction(0x12, 6, CPUType.CMOS), Indirect]
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