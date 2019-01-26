namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class STZ : InstructionBase
    {
        public STZ(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x64, 3, CPUType.CMOS), ZeroPage]
        [CPUInstruction(0x74, 4, CPUType.CMOS), ZeroPageX]
        [CPUInstruction(0x9C, 4, CPUType.CMOS), Absolute]
        [CPUInstruction(0x9E, 5, CPUType.CMOS), AbsoluteX]
        public void STZ_Memory(int address)
        {
            cpu.Memory.Set(address, 0);
        }
    }
}