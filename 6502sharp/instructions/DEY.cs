using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class DEY
    {
        private ICpu _cpu;

        public DEY(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x88, 2)]
        public void DEY_Implied()
        {
            _cpu.Y.Value--;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.Y.Value);
        }
    }
}