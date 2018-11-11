using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class TXS
    {
        private ICpu _cpu;

        public TXS(ICpu cpu)
        {
            _cpu = cpu;
        }
        
        [CPUInstruction(0x9A, 2)]
        public void TXS_Implied()
        {
            _cpu.SP.Value = _cpu.X.Value;
        }
    }
}