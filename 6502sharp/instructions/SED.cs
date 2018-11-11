namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class SED
    {
        private ICpu _cpu;

        public SED(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xF8, 2)]
        public void SED_Implied()
        {
            _cpu.SR.Decimal = true;
        }
    }
}