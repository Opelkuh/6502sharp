using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TXA : InstructionBase
    {
        public TXA(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x8A, 2)]
        public void TXA_Implied()
        {
            cpu.A.Value = cpu.X.Value;

            flags.SetNegativeAndZero(cpu.A.Value);
        }
    }
}