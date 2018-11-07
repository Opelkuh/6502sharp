using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class ASL
    {
        private ICpu _cpu;

        public ASL(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x0A, 2)]
        public void ASL_Accumulator()
        {
            _cpu.A.Value = process(_cpu.A.Value);
        }

        [CPUInstruction(0x06, 5), ZeroPage]
        [CPUInstruction(0x16, 6), ZeroPageX]
        [CPUInstruction(0x0E, 6), AbsoluteAddress]
        [CPUInstruction(0x1E, 7), AbsoluteAddressX]
        public void ASL_Memory(int address)
        {
            byte value = _cpu.Memory.Get(address);
            byte shifted = process(value);

            _cpu.Memory.Set(address, shifted);
        }

        private byte process(byte value)
        {
            int shifted = value << 1;

            FlagHelper.SetCarry(_cpu, shifted);
            FlagHelper.SetZero(_cpu, shifted);
            FlagHelper.SetNegative(_cpu, shifted);

            return (byte)(shifted);
        }
    }
}