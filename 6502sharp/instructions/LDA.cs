using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class LDA
    {
        private ICpu _cpu;

        public LDA(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xA9, 2)]
        public void LDA_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xA5, 3), ZeroPage]
        [CPUInstruction(0xB5, 4), ZeroPageX]
        [CPUInstruction(0xAD, 4), AbsoluteAddress]
        [CPUInstruction(0xBD, 4), AbsoluteAddressX]
        [CPUInstruction(0xB9, 4), AbsoluteAddressY]
        [CPUInstruction(0xA1, 6), IndirectXAddress]
        [CPUInstruction(0xB1, 5), IndirectYAddress]
        [CPUInstruction(0xB2, 6, CPUType.CMOS), IndirectAddress]
        public void LDA_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            _cpu.A.Value = value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.A.Value);
        }
    }
}