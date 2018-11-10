namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class PHA
    {
        private ICpu _cpu;

        public PHA(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x48, 3)]
        public void PHA_Implied()
        {
            _cpu.Stack.Push(_cpu.A.Value);
        }
    }
}