namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class STA
    {
        private ICpu _cpu;

        public STA(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x85, 3), ZeroPage]
        [CPUInstruction(0x95, 4), ZeroPageX]
        [CPUInstruction(0x8D, 4), Absolute]
        [CPUInstruction(0x9D, 5), AbsoluteX]
        [CPUInstruction(0x99, 5), AbsoluteY]
        [CPUInstruction(0x81, 6), IndirectX]
        [CPUInstruction(0x91, 6), IndirectY]
        [CPUInstruction(0x92, 6, CPUType.CMOS), Indirect]
        public void STA_Memory(int address)
        {
            _cpu.Memory.Set(address, _cpu.A.Value);
        }
    }
}