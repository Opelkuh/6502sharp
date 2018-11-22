namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CLD : InstructionBase
    {
        public CLD(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xD8, 2)]
        public void CLD_Implied()
        {
            cpu.SR.Decimal = false;
        }
    }
}