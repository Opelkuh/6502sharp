using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class DEX
    {
        private ICpu _cpu;

        public DEX(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xCA, 2)]
        public void DEX_Implied()
        {
            _cpu.X.Value--;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.Y.Value);
        }
    }
}