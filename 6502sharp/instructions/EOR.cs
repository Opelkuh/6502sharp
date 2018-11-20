using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class EOR
    {
        private ICpu _cpu;

        public EOR(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x49, 2)]
        public void EOR_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0x45, 3), ZeroPage]
        [CPUInstruction(0x55, 4), ZeroPageX]
        [CPUInstruction(0x4D, 4), Absolute]
        [CPUInstruction(0x5D, 4), AbsoluteX]
        [CPUInstruction(0x59, 4), AbsoluteY]
        [CPUInstruction(0x41, 6), IndirectX]
        [CPUInstruction(0x51, 5), IndirectY]
        [CPUInstruction(0x52, 6, CPUType.CMOS), Indirect]
        public void EOR_Memory(int address)
        {
            process(_cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            _cpu.A.Value ^= value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.A.Value);
        }
    }
}