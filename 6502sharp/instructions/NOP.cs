namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class NOP
    {
        private ICpu _cpu;

        public NOP(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xEA, 2)]
        public void NOP_Implied()
        {
            // do nothing :)
        }
    }
}