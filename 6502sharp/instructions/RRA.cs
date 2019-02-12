using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class RRA : InstructionBase
    {
        private ROR ror;
        private ADC adc;

        public RRA(ICpu cpu) : base(cpu)
        {
            this.ror = new ROR(cpu);
            this.adc = new ADC(cpu);
        }

        [NMOSOnly]
        [CPUInstruction(0x67, 5), ZeroPage]
        [CPUInstruction(0x77, 6), ZeroPageX]
        [CPUInstruction(0x6F, 6), Absolute]
        [CPUInstruction(0x7F, 7), AbsoluteX]
        [CPUInstruction(0x7B, 7), AbsoluteY]
        [CPUInstruction(0x63, 8), IndirectX]
        [CPUInstruction(0x73, 8), IndirectY]
        public void RRA_Memory(int address)
        {
            ror.ROR_Memory(address);
            bool carry = cpu.SR.Carry;

            adc.ADC_Memory(address);

            // set carry from ROR
            cpu.SR.Carry = carry;
        }
    }
}