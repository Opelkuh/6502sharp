namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BVS : InstructionBase
    {
        public BVS(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x70, 2)]
        public void BVS_Relative([Relative] int target)
        {
            if (cpu.SR.Overflow == true) cpu.PC.Value = (ushort)target;
        }
    }
}