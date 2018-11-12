using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class CPY
    {
        private ICpu _cpu;

        public CPY(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xC0, 2)]
        public void CPY_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xC4, 3), ZeroPage]
        [CPUInstruction(0xCC, 4), Absolute]
        public void CPY_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            CompareHelper.RegisterAndValue(_cpu, _cpu.Y.Value, value);
        }
    }
}