using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SLO : InstructionBase
    {
        private ASL asl;
        private ORA ora;

        public SLO(ICpu cpu) : base(cpu)
        {
            this.asl = new ASL(cpu);
            this.ora = new ORA(cpu);
        }

        [NMOSOnly]
        [CPUInstruction(0x07, 5), ZeroPage]
        [CPUInstruction(0x17, 6), ZeroPageX]
        [CPUInstruction(0x0F, 6), Absolute]
        [CPUInstruction(0x1F, 7), AbsoluteX]
        [CPUInstruction(0x1B, 7), AbsoluteY]
        [CPUInstruction(0x03, 8), IndirectX]
        [CPUInstruction(0x13, 8), IndirectY]
        public void SLO_Memory(int address)
        {
            asl.ASL_Memory(address);
            ora.ORA_Memory(address);
        }
    }
}