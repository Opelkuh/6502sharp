namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class JMP : InstructionBase
    {
        public JMP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x4C, 3), Absolute]
        [CPUInstruction(0x6C, 5), Indirect]
        [CPUInstruction(0x7C, 6, CPUType.CMOS), IndirectX]
        public void JMP_Memory(int address)
        {
            cpu.PC.Value = (ushort)address;
        }
    }
}