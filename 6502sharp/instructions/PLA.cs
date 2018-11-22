using _6502sharp.Helpers;

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

            FlagHelper.SetNegativeAndZero(cpu, cpu.A.Value);
        }
    }
}