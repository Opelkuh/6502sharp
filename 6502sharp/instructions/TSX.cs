using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TSX : InstructionBase
    {
        public TSX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xBA, 2)]
        public void TSX_Implied()
        {
            cpu.X.Value = cpu.SP.Value;

            flags.SetNegativeAndZero(cpu.X.Value);
        }
    }
}