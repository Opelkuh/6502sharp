using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BIT
    {
        private ICpu _cpu;

        public BIT(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x89, 2, CPUType.CMOS)]
        public void BIT_Immediate(byte target)
        {
            process(target);
        }

        [CPUInstruction(0x24, 3), ZeroPage]
        [CPUInstruction(0x2C, 4), Absolute]
        [CPUInstruction(0x3C, 4, CPUType.CMOS), AbsoluteX]
        [CPUInstruction(0x34, 4, CPUType.CMOS), ZeroPageX]
        public void BIT_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte target)
        {
            // set flags
            _cpu.SR.Negative = ((1 << 7) & target) > 0;
            _cpu.SR.Overflow = ((1 << 6) & target) > 0;

            // AND accu and target value
            int res = _cpu.A.Value & target;
            FlagHelper.SetZero(_cpu, res);
        }
    }
}