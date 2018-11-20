using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class PLA
    {
        private ICpu _cpu;

        public PLA(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x68, 4)]
        public void PLA_Implied()
        {
            _cpu.A.Value = _cpu.Stack.Pop();

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.A.Value);
        }
    }
}