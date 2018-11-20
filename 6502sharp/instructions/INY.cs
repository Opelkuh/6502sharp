using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
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

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.Y.Value);
        }
    }
}