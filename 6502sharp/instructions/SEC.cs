namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SEC : InstructionBase
    {
        public SEC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x38, 2)]
        public void SEC_Implied()
        {
            cpu.SR.Carry = true;
        }
    }
}