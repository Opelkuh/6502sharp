using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BIT : InstructionBase
    {
        public BIT(ICpu cpu) : base(cpu)
        {
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
            process(cpu.Memory.Get(address));
        }

        private void process(byte target)
        {
            // set flags
            cpu.SR.Negative = ((1 << 7) & target) > 0;
            cpu.SR.Overflow = ((1 << 6) & target) > 0;

            // AND accu and target value
            int res = cpu.A.Value & target;
            FlagHelper.SetZero(cpu, res);
        }
    }
}