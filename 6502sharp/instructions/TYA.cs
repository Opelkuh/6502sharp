using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TYA
    {
        private ICpu _cpu;

        public TYA(ICpu cpu)
        {
            _cpu = cpu;
        }
        
        [CPUInstruction(0x98, 2)]
        public void TYA_Implied()
        {
            _cpu.A.Value = _cpu.Y.Value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.A.Value);
        }
    }
}