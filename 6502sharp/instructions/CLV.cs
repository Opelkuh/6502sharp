using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CLV : InstructionBase
    {
        public CLV(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xB8, 2)]
        public void CLV_Implied()
        {
            cpu.SR.Overflow = false;
        }
    }
}