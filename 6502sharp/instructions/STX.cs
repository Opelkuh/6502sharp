namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class STX
    {
        private ICpu _cpu;

        public STX(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x86, 3), ZeroPage]
        [CPUInstruction(0x96, 4), ZeroPageY]
        [CPUInstruction(0x8E, 4), Absolute]
        public void STX_Memory(int address)
        {
            _cpu.Memory.Set(address, _cpu.X.Value);
        }
    }
}