namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class NOP : InstructionBase
    {
        public NOP(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xEA, 2)]
        public void NOP_Implied()
        {
            // do nothing :)
        }

        [NMOSOnly]
        [CPUInstruction(0x80, 2), ZeroPage]
        [CPUInstruction(0x82, 2), ZeroPage]
        [CPUInstruction(0x89, 2), ZeroPage]
        [CPUInstruction(0xC2, 2), ZeroPage]
        [CPUInstruction(0xE2, 2), ZeroPage]
        [CPUInstruction(0x04, 3), ZeroPage]
        [CPUInstruction(0x44, 3), ZeroPage]
        [CPUInstruction(0x64, 3), ZeroPage]
        [CPUInstruction(0x14, 4), ZeroPageX]
        [CPUInstruction(0x34, 4), ZeroPageX]
        [CPUInstruction(0x54, 4), ZeroPageX]
        [CPUInstruction(0x74, 4), ZeroPageX]
        [CPUInstruction(0xD4, 4), ZeroPageX]
        [CPUInstruction(0xF4, 4), ZeroPageX]
        [CPUInstruction(0x0C, 4), Absolute]
        [CPUInstruction(0x1C, 4), AbsoluteX]
        [CPUInstruction(0x3C, 4), AbsoluteX]
        [CPUInstruction(0x5C, 4), AbsoluteX]
        [CPUInstruction(0x7C, 4), AbsoluteX]
        [CPUInstruction(0xDC, 4), AbsoluteX]
        [CPUInstruction(0xFC, 4), AbsoluteX]
        public void NOP_NMOS(int address)
        {
            // do nothing but NMOS :)
        }

        [CMOSOnly]
        [CPUInstruction(0x02, 2), ZeroPage]
        [CPUInstruction(0x22, 2), ZeroPage]
        [CPUInstruction(0x42, 2), ZeroPage]
        [CPUInstruction(0x62, 2), ZeroPage]
        [CPUInstruction(0x82, 2), ZeroPage]
        [CPUInstruction(0xC2, 2), ZeroPage]
        [CPUInstruction(0xE2, 2), ZeroPage]
        [CPUInstruction(0x44, 3), ZeroPageX]
        [CPUInstruction(0x54, 4), ZeroPageX]
        [CPUInstruction(0xD4, 4), ZeroPageX]
        [CPUInstruction(0xF4, 4), ZeroPageX]
        [CPUInstruction(0x5C, 8), Absolute]
        [CPUInstruction(0xDC, 4), Absolute]
        [CPUInstruction(0xFC, 4), Absolute]
        public void NOP_CMOS(int address)
        {
            // do nothing but CMOS :)
        }
    }
}