using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class ADC
    {
        private ICpu _cpu;

        public ADC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x69, 2)]
        public void ADC_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0x65, 3), ZeroPage]
        [CPUInstruction(0x75, 4), ZeroPageX]
        [CPUInstruction(0x6D, 4), AbsoluteAddress]
        [CPUInstruction(0x7D, 4), AbsoluteAddressX]
        [CPUInstruction(0x79, 4), AbsoluteAddressY]
        [CPUInstruction(0x61, 6), IndirectXAddress]
        [CPUInstruction(0x71, 5), IndirectYAddress]
        [CPUInstruction(0x72, 6, CPUType.CMOS), IndirectAddress]
        public void ADC_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            int carry = _cpu.SR.Carry ? 1 : 0;
            int res = value + _cpu.A.Value + carry;

            FlagHelper.SetCarry(_cpu, res);
            FlagHelper.SetZero(_cpu, res);
            FlagHelper.SetNegative(_cpu, res);
            FlagHelper.SetOverflow(_cpu, res, _cpu.A.Value, value + carry);

            if (_cpu.DecimalMode && _cpu.SR.Decimal)
            {
                BCDHelper.AdditionAdjust(_cpu, ref res, value, _cpu.A.Value, carry);
            }

            _cpu.A.Value = (byte)(res & 0xFF);
        }
    }
}