namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SEI
    {
        private ICpu _cpu;

        public SEI(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x78, 2)]
        public void SEI_Implied()
        {
            _cpu.SR.Interrupt = true;
        }
    }
}