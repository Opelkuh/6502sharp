using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SED : InstructionBase
    {
        public SED(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xF8, 2)]
        public void SED_Implied()
        {
            cpu.SR.Decimal = true;
        }
    }
}