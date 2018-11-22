using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class INX : InstructionBase
    {
        public INX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xE8, 2)]
        public void INX_Implied()
        {
            cpu.X.Value++;

            FlagHelper.SetNegativeAndZero(cpu, cpu.X.Value);
        }
    }
}