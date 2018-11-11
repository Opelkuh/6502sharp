using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class STY
    {
        private ICpu _cpu;

        public STY(ICpu cpu)
        {
            _cpu = cpu;
        }
        
        [CPUInstruction(0x84, 3), ZeroPage]
        [CPUInstruction(0x94, 4), ZeroPageX]
        [CPUInstruction(0x8C, 4), AbsoluteAddress]
        public void STY_Memory(int address)
        {
            _cpu.Memory.Set(address, _cpu.Y.Value);
        }
    }
}