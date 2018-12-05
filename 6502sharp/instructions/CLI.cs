namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CLI : InstructionBase
    {
        public CLI(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x58, 2)]
        public void CLI_Implied()
        {
            cpu.SR.Interrupt = false;
        }
    }
}