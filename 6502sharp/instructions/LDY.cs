using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class LDY
    {
        private ICpu _cpu;

        public LDY(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xA0, 2)]
        public void LDY_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xA4, 3), ZeroPage]
        [CPUInstruction(0xB4, 4), ZeroPageX]
        [CPUInstruction(0xAC, 4), Absolute]
        [CPUInstruction(0xBC, 4), AbsoluteX]
        public void LDY_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            _cpu.Y.Value = value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.Y.Value);
        }
    }
}