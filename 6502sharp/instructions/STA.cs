using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class STA
    {
        private ICpu _cpu;

        public STA(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x85, 3), ZeroPage]
        [CPUInstruction(0x95, 4), ZeroPageX]
        [CPUInstruction(0x8D, 4), AbsoluteAddress]
        [CPUInstruction(0x9D, 5), AbsoluteAddressX]
        [CPUInstruction(0x99, 5), AbsoluteAddressY]
        [CPUInstruction(0x81, 6), IndirectXAddress]
        [CPUInstruction(0x91, 6), IndirectYAddress]
        [CPUInstruction(0x92, 6, CPUType.CMOS), IndirectAddress]
        public void STA_Memory(int address)
        {
            _cpu.Memory.Set(address, _cpu.A.Value);
        }
    }
}