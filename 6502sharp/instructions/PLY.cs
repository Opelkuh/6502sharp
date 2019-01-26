using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PLY : InstructionBase
    {
        public PLY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x7A, 4, CPUType.CMOS)]
        public void PLY_Implied()
        {
            cpu.Y.Value = cpu.Stack.Pop();

            flags.SetNegativeAndZero(cpu.Y.Value);
        }
    }
}