using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PHY : InstructionBase
    {
        public PHY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x5A, 3, CPUType.CMOS)]
        public void PHX_Implied()
        {
            cpu.Stack.Push(cpu.Y.Value);
        }
    }
}