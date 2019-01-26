using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PLX : InstructionBase
    {
        public PLX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xFA, 4, CPUType.CMOS)]
        public void PLX_Implied()
        {
            cpu.X.Value = cpu.Stack.Pop();

            flags.SetNegativeAndZero(cpu.X.Value);
        }
    }
}