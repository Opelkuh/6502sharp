using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class INX
    {
        private ICpu _cpu;

        public INX(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xE8, 2)]
        public void INX_Implied()
        {
            _cpu.X.Value++;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.X.Value);
        }
    }
}