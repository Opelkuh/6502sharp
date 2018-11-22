using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class INY : InstructionBase
    {
        public INY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xC8, 2)]
        public void INY_Implied()
        {
            cpu.Y.Value++;

            FlagHelper.SetNegativeAndZero(cpu, cpu.Y.Value);
        }
    }
}