namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BCS : InstructionBase
    {
        public BCS(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xB0, 2)]
        public void BCS_Relative([Relative] int target)
        {
            if (cpu.SR.Carry == true) cpu.PC.Value = target;
        }
    }
}