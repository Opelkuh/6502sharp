using _6502sharp.Helpers;

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

            FlagHelper.SetNegativeAndZero(cpu, cpu.X.Value);
        }
    }
}