using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ADC : InstructionBase
    {
        public ADC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x69, 2)]
        public void ADC_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0x65, 3), ZeroPage]
        [CPUInstruction(0x75, 4), ZeroPageX]
        [CPUInstruction(0x6D, 4), Absolute]
        [CPUInstruction(0x7D, 4), AbsoluteX]
        [CPUInstruction(0x79, 4), AbsoluteY]
        [CPUInstruction(0x61, 6), IndirectX]
        [CPUInstruction(0x71, 5), IndirectY]
        [CPUInstruction(0x72, 6, CPUType.CMOS), Indirect]
        public void ADC_Memory(int address)
        {
            process(cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            int carry = cpu.SR.Carry ? 1 : 0;
            int res = value + cpu.A.Value + carry;

            flags
                .SetCarry(res)
                .SetZero(res)
                .SetNegative(res)
                .SetOverflow(res, cpu.A.Value, value + carry);

            if (cpu.DecimalMode && cpu.SR.Decimal)
            {
                BCDHelper.AdditionAdjust(cpu, ref res, value, cpu.A.Value, carry);
            }

            cpu.A.Value = (byte)(res & 0xFF);
        }
    }
}