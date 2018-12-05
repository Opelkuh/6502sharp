using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TXS : InstructionBase
    {
        public TXS(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x9A, 2)]
        public void TXS_Implied()
        {
            cpu.SP.Value = cpu.X.Value;
        }
    }
}