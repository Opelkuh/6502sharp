namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class DOP : InstructionBase
    {
        public DOP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x80, 2)]
        [CPUInstruction(0x82, 2)]
        [CPUInstruction(0x89, 2)]
        [CPUInstruction(0xC2, 2)]
        [CPUInstruction(0xE2, 2)]
        public void DOP_Immediate(byte value)
        {
            // do nothing :)
        }


        [CPUInstruction(0x04, 3), ZeroPage]
        [CPUInstruction(0x44, 3), ZeroPage]
        [CPUInstruction(0x64, 3), ZeroPage]
        [CPUInstruction(0x14, 4), ZeroPageX]
        [CPUInstruction(0x34, 4), ZeroPageX]
        [CPUInstruction(0x54, 4), ZeroPageX]
        [CPUInstruction(0x74, 4), ZeroPageX]
        [CPUInstruction(0xD4, 4), ZeroPageX]
        [CPUInstruction(0xF4, 4), ZeroPageX]
        public void DOP_Address(int address)
        {
            // do nothing :)
        }
    }
}