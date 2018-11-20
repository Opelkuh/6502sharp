namespace _6502sharp.Instructions
{
    [DefaultInstruction]
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
            _cpu.SR.Break = true;

            _cpu.Stack.Push(_cpu.SR.Value);
        }
    }
}