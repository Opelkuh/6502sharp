using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class ROL
    {
        private ICpu _cpu;

        public ROL(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x2A, 2)]
        public void ROL_Accumulator()
        {
            _cpu.A.Value = process(_cpu.A.Value);
        }

        [CPUInstruction(0x26, 5), ZeroPage]
        [CPUInstruction(0x36, 6), ZeroPageX]
        [CPUInstruction(0x2E, 6), AbsoluteAddress]
        [CPUInstruction(0x3E, 7), AbsoluteAddressX]
        public void ROL_Memory(int address)
        {
            byte rotated = process(_cpu.Memory.Get(address));

            _cpu.Memory.Set(address, rotated);
        }

        private byte process(byte value)
        {
            int res = (value << 1) | (_cpu.SR.Carry ? 1 : 0);
            bool newCarry = (value & (1 << 7)) > 0;

            _cpu.SR.Carry = newCarry;
            FlagHelper.SetNegativeAndZero(_cpu, res);

            return (byte)res;
        }
    }
}