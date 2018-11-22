using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class DEX : InstructionBase
    {
        public DEX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xCA, 2)]
        public void DEX_Implied()
        {
            cpu.X.Value--;

            FlagHelper.SetNegativeAndZero(cpu, cpu.X.Value);
        }
    }
}