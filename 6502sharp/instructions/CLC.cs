using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CLC : InstructionBase
    {
        public CLC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x18, 2)]
        public void CLC_Implied()
        {
            cpu.SR.Carry = false;
        }
    }
}