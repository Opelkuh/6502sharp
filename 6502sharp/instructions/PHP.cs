namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class PHP
    {
        private ICpu _cpu;

        public PHP(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x08, 3)]
        public void PHP_Implied()
        {
            _cpu.Stack.Push(_cpu.SR.Value);
        }
    }
}