namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class JSR
    {
        private ICpu _cpu;

        public JSR(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x20, 6)]
        public void JSR_Memory([Absolute] int address)
        {
            // decrement pc to last byte of JSR instruction
            _cpu.PC.Value--;
            _cpu.Stack.PushPC();

            // jump
            _cpu.PC.Value = address;
        }
    }
}