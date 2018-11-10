namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class DEY
    {
        private ICpu _cpu;

        public DEY(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x88, 2)]
        public void DEY_Implied(int address)
        {
            _cpu.Y.Value--;
        }
    }
}