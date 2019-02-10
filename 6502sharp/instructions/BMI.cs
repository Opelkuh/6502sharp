namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BMI : InstructionBase
    {
        public BMI(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x30, 2)]
        public void BMI_Relative([Relative] int target)
        {
            if (cpu.SR.Negative == true) branch.Branch(target);
        }
    }
}