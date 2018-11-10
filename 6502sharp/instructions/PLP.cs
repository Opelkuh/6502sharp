using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class PLP
    {
        private ICpu _cpu;

        public PLP(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x28, 4)]
        public void PLP_Implied()
        {
            _cpu.SR.Value = _cpu.Stack.Pop();
        }
    }
}