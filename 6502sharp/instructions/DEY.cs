using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class DEY : InstructionBase
    {
        public DEY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x88, 2)]
        public void DEY_Implied()
        {
            cpu.Y.Value--;

            flags.SetNegativeAndZero(cpu.Y.Value);
        }
    }
}