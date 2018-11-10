namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class DEC
    {
        private ICpu _cpu;

        public DEC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x3A, 2, CPUType.CMOS)]
        public void DEC_Accumulator() {
            _cpu.A.Value--;
        }

        [CPUInstruction(0xC6, 5), ZeroPage]
        [CPUInstruction(0xD6, 6), ZeroPageX]
        [CPUInstruction(0xCE, 3), AbsoluteAddress]
        [CPUInstruction(0xDE, 7), AbsoluteAddressX]
        public void DEC_Memory(int address)
        {
            byte val = _cpu.Memory.Get(address);

            _cpu.Memory.Set(address, --val);
        }
    }
}