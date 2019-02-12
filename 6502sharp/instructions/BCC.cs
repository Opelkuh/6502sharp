using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BCC : InstructionBase
    {
        public BCC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x90, 2)]
        public void BCC_Relative([Relative] int target)
        {
            if (cpu.SR.Carry == false) branch.Branch(target);
        }
    }
}