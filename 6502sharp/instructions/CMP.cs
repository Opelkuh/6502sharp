using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class CMP
    {
        private ICpu _cpu;

        public CMP(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xC9, 2)]
        public void CMP_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xC5, 3), ZeroPage]
        [CPUInstruction(0xD5, 4), ZeroPageX]
        [CPUInstruction(0xCD, 4), AbsoluteAddress]
        [CPUInstruction(0xDD, 4), AbsoluteAddressX]
        [CPUInstruction(0xD9, 4), AbsoluteAddressY]
        [CPUInstruction(0xC1, 6), IndirectXAddress]
        [CPUInstruction(0xD1, 5), IndirectYAddress]
        [CPUInstruction(0xD2, 6, CPUType.CMOS), IndirectAddress]
        public void CMP_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            CompareHelper.RegisterAndValue(_cpu, _cpu.A.Value, value);
        }
    }
}