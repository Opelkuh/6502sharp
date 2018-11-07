using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class BIT
    {
        private ICpu _cpu;

        public BIT(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x24, 3), ZeroPage]
        [CPUInstruction(0x2C, 4), AbsoluteAddress]
        public void BIT_Memory(int address)
        {
            byte target = _cpu.Memory.Get(address);

            // set flags
            _cpu.SR.Negative = ((1 << 7) & target) > 0;
            _cpu.SR.Overflow = ((1 << 6) & target) > 0;

            // AND accu and target value
            int res = _cpu.A.Value & target;
            FlagHelper.SetZero(_cpu, res);
        }
    }
}