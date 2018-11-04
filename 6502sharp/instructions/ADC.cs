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
        public void ADC_Immidiate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0x65, 3)]
        public void ADC_Zeropage([ZeroPage] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        [CPUInstruction(0x75, 4)]
        public void ADC_ZeropageX([ZeroPageX] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        [CPUInstruction(0x6D, 4)]
        public void ADC_Absolute([AbsoluteAddress] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        [CPUInstruction(0x7D, 4)]
        public void ADC_AbsoluteX([AbsoluteAddressX] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        [CPUInstruction(0x79, 4)]
        public void ADC_AbsoluteY([AbsoluteAddressY] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        [CPUInstruction(0x61, 6)]
        public void ADC_IndirectX([IndirectXAddress] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        [CPUInstruction(0x71, 5)]
        public void ADC_IndirectY([IndirectYAddress] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        [CPUInstruction(0x72, 6, CPUType.CMOS)]
        public void ADC_Indirect([IndirectAddress] int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            int carry = _cpu.SR.Carry ? 1 : 0;
            int res = value + _cpu.A.Value + carry;

            if (_cpu.DecimalMode && _cpu.SR.Decimal)
            {
                BCDHelper.AdditionAdjust(_cpu, ref res, value, _cpu.A.Value, carry);
            }

            _cpu.A.Value = (byte)(res & 0xFF);
        }
    }
}