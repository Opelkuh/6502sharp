using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CMP : InstructionBase
    {
        public CMP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xC9, 2)]
        public void CMP_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xC5, 3), ZeroPage]
        [CPUInstruction(0xD5, 4), ZeroPageX]
        [CPUInstruction(0xCD, 4), Absolute]
        [CPUInstruction(0xDD, 4), AbsoluteX]
        [CPUInstruction(0xD9, 4), AbsoluteY]
        [CPUInstruction(0xC1, 6), IndirectX]
        [CPUInstruction(0xD1, 5), IndirectY]
        [CPUInstruction(0xD2, 6, CPUType.CMOS), Indirect]
        public void CMP_Memory(int address)
        {
            process(cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            compare.RegisterAndValue(cpu.A.Value, value);
        }
    }
}