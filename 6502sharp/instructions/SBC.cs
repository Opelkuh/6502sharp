using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class SBC
    {
        private ICpu _cpu;

        public SBC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xE9, 2)]
        public void SBC_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xE5, 3), ZeroPage]
        [CPUInstruction(0xF5, 4), ZeroPageX]
        [CPUInstruction(0xED, 4), AbsoluteAddress]
        [CPUInstruction(0xFD, 4), AbsoluteAddressX]
        [CPUInstruction(0xF9, 4), AbsoluteAddressY]
        [CPUInstruction(0xE1, 6), IndirectXAddress]
        [CPUInstruction(0xF1, 5), IndirectYAddress]
        [CPUInstruction(0xF2, 6, CPUType.CMOS), IndirectAddress]
        public void SBC_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            int carry = _cpu.SR.Carry ? 0 : 1;
            int res = _cpu.A.Value - value - carry;
            int wrapped = (byte)res;

            _cpu.SR.Carry = res >= 0;

            FlagHelper.SetZero(_cpu, wrapped);
            FlagHelper.SetNegative(_cpu, wrapped);
            FlagHelper.SetOverflow(_cpu, wrapped, _cpu.A.Value, value - carry);

            if (_cpu.DecimalMode && _cpu.SR.Decimal)
            {
                BCDHelper.SubstractionAdjust(_cpu, ref wrapped, _cpu.A.Value, value, carry);
            }

            _cpu.A.Value = (byte)(wrapped & 0xFF);
        }
    }
}