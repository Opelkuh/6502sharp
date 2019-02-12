using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BVC : InstructionBase
    {
        public BVC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x50, 2)]
        public void BVC_Relative([Relative] int target)
        {
            if (cpu.SR.Overflow == false) branch.Branch(target);
        }
    }
}