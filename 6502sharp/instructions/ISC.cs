using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ISC : InstructionBase
    {
        private SBC sbc;

        public ISC(ICpu cpu) : base(cpu)
        {
            this.sbc = new SBC(cpu);
        }

        [NMOSOnly]
        [CPUInstruction(0xE7, 5), ZeroPage]
        [CPUInstruction(0xF7, 6), ZeroPageX]
        [CPUInstruction(0xEF, 6), Absolute]
        [CPUInstruction(0xFF, 7), AbsoluteX]
        [CPUInstruction(0xFB, 7), AbsoluteY]
        [CPUInstruction(0xE3, 8), IndirectX]
        [CPUInstruction(0xF3, 8), IndirectY]
        public void ISC_Memory(int address)
        {
            byte value = cpu.Memory.Get(address);

            value++;

            sbc.SBC_Immediate(value);
        }
    }
}