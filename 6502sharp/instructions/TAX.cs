using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TAX
    {
        private ICpu _cpu;

        public TAX(ICpu cpu)
        {
            _cpu = cpu;
        }
        
        [CPUInstruction(0xAA, 2)]
        public void TAX_Implied()
        {
            _cpu.X.Value = _cpu.A.Value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.X.Value);
        }
    }
}