using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class INC
    {
        private ICpu _cpu;

        public INC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x1A, 2, CPUType.CMOS)]
        public void INC_Accumulator()
        {
            _cpu.A.Value++;

            FlagHelper.SetNegative(_cpu, _cpu.A.Value);
            FlagHelper.SetZero(_cpu, _cpu.A.Value);
        }

        [CPUInstruction(0xE6, 5), ZeroPage]
        [CPUInstruction(0xF6, 6), ZeroPageX]
        [CPUInstruction(0xEE, 6), AbsoluteAddress]
        [CPUInstruction(0xFE, 7), AbsoluteAddressX]
        public void INC_Memory(int address)
        {
            byte val = _cpu.Memory.Get(address);

            val++;

            FlagHelper.SetNegativeAndZero(_cpu, val);

            _cpu.Memory.Set(address, val);
        }
    }
}