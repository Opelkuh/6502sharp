using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SBC : InstructionBase
    {
        public SBC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xE9, 2)]
        [CPUInstruction(0xEB, 2, CPUType.NMOS)]
        public void SBC_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xE5, 3), ZeroPage]
        [CPUInstruction(0xF5, 4), ZeroPageX]
        [CPUInstruction(0xED, 4), Absolute]
        [CPUInstruction(0xFD, 4), AbsoluteX]
        [CPUInstruction(0xF9, 4), AbsoluteY]
        [CPUInstruction(0xE1, 6), IndirectX]
        [CPUInstruction(0xF1, 5), IndirectY]
        [CPUInstruction(0xF2, 6, CPUType.CMOS), Indirect]
        public void SBC_Memory(int address)
        {
            process(cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            int carry = cpu.SR.Carry ? 0 : 1;
            int res = cpu.A.Value - value - carry;
            int wrapped = (byte)res;

            cpu.SR.Carry = res >= 0;

            flags
                .SetZero(wrapped)
                .SetNegative(wrapped)
                .SetOverflow(wrapped, cpu.A.Value, value - carry);

            if (cpu.DecimalMode && cpu.SR.Decimal)
            {
                bcd.SubstractionAdjust(ref wrapped, cpu.A.Value, value, carry);
            }

            cpu.A.Value = (byte)(wrapped & 0xFF);
        }
    }
}