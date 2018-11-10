using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class INY
    {
        private ICpu _cpu;

        public INY(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xC8, 2)]
        public void INY_Implied()
        {
            _cpu.Y.Value++;

            FlagHelper.SetNegative(_cpu, _cpu.Y.Value);
            FlagHelper.SetZero(_cpu, _cpu.Y.Value);
        }
    }
}