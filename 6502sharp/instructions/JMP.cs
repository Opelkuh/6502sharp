namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class JMP
    {
        private ICpu _cpu;

        public JMP(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x4C, 3), Absolute]
        [CPUInstruction(0x6C, 5), Indirect]
        [CPUInstruction(0x7C, 6, CPUType.CMOS), IndirectX]
        public void JMP_Memory(int address)
        {
            _cpu.PC.Value = address;
        }
    }
}