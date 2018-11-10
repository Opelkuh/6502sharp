using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class LSR
    {
        private ICpu _cpu;

        public LSR(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x4A, 2)]
        public void LSR_Accumulator()
        {
            _cpu.A.Value = process(_cpu.A.Value);
        }

        [CPUInstruction(0x46, 5), ZeroPage]
        [CPUInstruction(0x56, 6), ZeroPageX]
        [CPUInstruction(0x4E, 6), AbsoluteAddress]
        [CPUInstruction(0x5E, 7), AbsoluteAddressX]
        public void LSR_Memory(int address)
        {
            byte shifted = process(_cpu.Memory.Get(address));

            _cpu.Memory.Set(address, shifted);
        }

        private byte process(byte value)
        {
            bool carry = (value & 0x01) > 0;
            int res = value >> 1;

            _cpu.SR.Carry = carry;
            _cpu.SR.Negative = false;
            FlagHelper.SetZero(_cpu, res);

            return (byte)res;
        }
    }
}