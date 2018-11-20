namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SEC
    {
        private ICpu _cpu;

        public SEC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x38, 2)]
        public void SEC_Implied()
        {
            _cpu.SR.Carry = true;
        }
    }
}