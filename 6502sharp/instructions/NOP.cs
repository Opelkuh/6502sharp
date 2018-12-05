namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class NOP : InstructionBase
    {
        public NOP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xEA, 2)]
        public void NOP_Implied()
        {
            // do nothing :)
        }
    }
}