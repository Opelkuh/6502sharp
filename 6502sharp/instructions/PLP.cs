using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PLP : InstructionBase
    {
        public PLP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x28, 4)]
        public void PLP_Implied()
        {
            cpu.SR.Value = cpu.Stack.Pop();
        }
    }
}