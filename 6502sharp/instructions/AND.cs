using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class AND
    {
        private ICpu _cpu;

        public AND(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x29, 2)]
        public void AND_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0x25, 3), ZeroPage]
        [CPUInstruction(0x35, 4), ZeroPageX]
        [CPUInstruction(0x2D, 4), Absolute]
        [CPUInstruction(0x3D, 4), AbsoluteX]
        [CPUInstruction(0x39, 4), AbsoluteY]
        [CPUInstruction(0x21, 6), IndirectX]
        [CPUInstruction(0x31, 5), IndirectY]
        [CPUInstruction(0x32, 6, CPUType.CMOS), Indirect]
        public void AND_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            byte res = (byte)(_cpu.A.Value & value);

            FlagHelper.SetZero(_cpu, res);
            FlagHelper.SetNegative(_cpu, res);

            _cpu.A.Value = (byte)(res & 0xFF);
        }
    }
}