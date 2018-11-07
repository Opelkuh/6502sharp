namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class CLD
    {
        private ICpu _cpu;

        public CLD(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xD8, 2)]
        public void CLD_Implied()
        {
            _cpu.SR.Decimal = false;
        }
    }
}