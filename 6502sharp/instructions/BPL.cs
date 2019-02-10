namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BPL : InstructionBase
    {
        public BPL(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x10, 2)]
        public void BPL_Relative([Relative] int target)
        {
            if (cpu.SR.Negative == false) branch.Branch(target);
        }
    }
}