using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CPX
    {
        private ICpu _cpu;

        public CPX(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xE0, 2)]
        public void CPX_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xE4, 3), ZeroPage]
        [CPUInstruction(0xEC, 4), Absolute]
        public void CPX_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            CompareHelper.RegisterAndValue(_cpu, _cpu.X.Value, value);
        }
    }
}