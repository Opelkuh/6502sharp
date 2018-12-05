using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class RTS : InstructionBase
    {
        public RTS(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x60, 6)]
        public void RTS_Implied()
        {
            cpu.PC.Value = (ushort)cpu.Stack.PopPC();
            cpu.PC.Value++;
        }
    }
}