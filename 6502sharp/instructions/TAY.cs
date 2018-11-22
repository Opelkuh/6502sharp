using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TAY : InstructionBase
    {
        public TAY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xA8, 2)]
        public void TAY_Implied()
        {
            cpu.Y.Value = cpu.A.Value;

            flags.SetNegativeAndZero(cpu.Y.Value);
        }
    }
}