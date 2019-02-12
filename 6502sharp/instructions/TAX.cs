using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TAX : InstructionBase
    {
        public TAX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xAA, 2)]
        public void TAX_Implied()
        {
            cpu.X.Value = cpu.A.Value;

            flags.SetNegativeAndZero(cpu.X.Value);
        }
    }
}