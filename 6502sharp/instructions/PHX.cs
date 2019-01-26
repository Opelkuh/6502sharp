namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PHX : InstructionBase
    {
        public PHX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xDA, 3, CPUType.CMOS)]
        public void PHX_Implied()
        {
            cpu.Stack.Push(cpu.X.Value);
        }
    }
}