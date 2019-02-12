using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class JSR : InstructionBase
    {
        public JSR(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x20, 6)]
        public void JSR_Memory([Absolute] int address)
        {
            // decrement pc to last byte of JSR instruction
            cpu.PC.Value--;
            cpu.Stack.PushPC();

            // jump
            cpu.PC.Value = (ushort)address;
        }
    }
}