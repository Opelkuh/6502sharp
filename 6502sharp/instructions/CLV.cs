namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CLV
    {
        private ICpu _cpu;

        public CLV(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xB8, 2)]
        public void CLV_Implied()
        {
            _cpu.SR.Overflow = false;
        }
    }
}