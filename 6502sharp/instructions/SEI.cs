using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SEI : InstructionBase
    {
        public SEI(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x78, 2)]
        public void SEI_Implied()
        {
            cpu.SR.Interrupt = true;
        }
    }
}