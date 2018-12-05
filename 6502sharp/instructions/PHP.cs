namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PHP : InstructionBase
    {
        public PHP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x08, 3)]
        public void PHP_Implied()
        {
            cpu.SR.Break = true;

            cpu.Stack.Push(cpu.SR.Value);
        }
    }
}