using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PHP : InstructionBase
    {
        public PHP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x08, 3)]
        public void PHP_Implied()   
        {
            cpu.Stack.Push((byte)(cpu.SR.Value | (int)StatusFlag.Break));
        }
    }
}