using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PLA : InstructionBase
    {
        public PLA(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x68, 4)]
        public void PLA_Implied()
        {
            cpu.A.Value = cpu.Stack.Pop();

            flags.SetNegativeAndZero(cpu.A.Value);
        }
    }
}