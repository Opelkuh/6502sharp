using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class STX
    {
        private ICpu _cpu;

        public STX(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x86, 3), ZeroPage]
        [CPUInstruction(0x96, 4), ZeroPageY]
        [CPUInstruction(0x8E, 4), AbsoluteAddress]
        public void STX_Memory(int address)
        {
            _cpu.Memory.Set(address, _cpu.X.Value);
        }
    }
}