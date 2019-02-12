using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BNE : InstructionBase
    {
        public BNE(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xD0, 2)]
        public void BNE_Relative([Relative] int target)
        {
            if (cpu.SR.Zero == false) branch.Branch(target);
        }
    }
}