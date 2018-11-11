using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class RTS
    {
        private ICpu _cpu;

        public RTS(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x60, 6)]
        public void RTS_Implied()
        {
            _cpu.PC.Value = _cpu.Stack.PopPC();
            _cpu.PC.Value++;
        }
    }
}