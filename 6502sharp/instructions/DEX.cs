using _6502sharp.Reflection;

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

            flags.SetNegativeAndZero(cpu.X.Value);
        }
    }
}