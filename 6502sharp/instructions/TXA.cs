using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TXA
    {
        private ICpu _cpu;

        public TXA(ICpu cpu)
        {
            _cpu = cpu;
        }
        
        [CPUInstruction(0x8A, 2)]
        public void TXA_Implied()
        {
            _cpu.A.Value = _cpu.X.Value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.A.Value);
        }
    }
}