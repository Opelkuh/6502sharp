namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PHA : InstructionBase
    {
        public PHA(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x48, 3)]
        public void PHA_Implied()
        {
            cpu.Stack.Push(cpu.A.Value);
        }
    }
}