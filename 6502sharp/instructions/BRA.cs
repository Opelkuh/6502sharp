using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BRA : InstructionBase
    {
        public BRA(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x80, 2, CPUType.CMOS)]
        public void BRA_Relative([Relative] int target)
        {
            cpu.PC.Value = (ushort)target;
        }
    }
}