namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BEQ : InstructionBase
    {
        public BEQ(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xF0, 2)]
        public void BEQ_Relative([Relative] int target)
        {
            if (cpu.SR.Zero == true) cpu.PC.Value = target;
        }
    }
}