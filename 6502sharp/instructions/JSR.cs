namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class JSR
    {
        private ICpu _cpu;

        public JSR(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x20, 6)]
        public void JSR_Memory([AbsoluteAddress] int address)
        {
            // decrement pc to last byte of JSR instruction
            _cpu.PC.Value--;
            _cpu.Stack.PushPC();

            // jump
            _cpu.PC.Value = address;
        }
    }
}