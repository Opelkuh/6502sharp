using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class RTI : InstructionBase
    {
        public RTI(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x40, 6)]
        public void RTI_Implied()
        {
            cpu.SR.Value = cpu.Stack.Pop();
            cpu.PC.Value = cpu.Stack.PopPC();
        }
    }
}