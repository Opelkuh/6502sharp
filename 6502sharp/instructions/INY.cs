using _6502sharp.Reflection;

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

            flags.SetNegativeAndZero(cpu.Y.Value);
        }
    }
}