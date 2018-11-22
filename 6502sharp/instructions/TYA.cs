using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TYA : InstructionBase
    {
        public TYA(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x98, 2)]
        public void TYA_Implied()
        {
            cpu.A.Value = cpu.Y.Value;

            flags.SetNegativeAndZero(cpu.A.Value);
        }
    }
}