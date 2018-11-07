namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class CLI
    {
        private ICpu _cpu;

        public CLI(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x58, 2)]
        public void CLI_Implied()
        {
            _cpu.SR.Interrupt = false;
        }
    }
}