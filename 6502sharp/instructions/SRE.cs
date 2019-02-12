using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SRE : InstructionBase
    {
        private LSR lsr;
        private EOR eor;

        public SRE(ICpu cpu) : base(cpu)
        {
            this.lsr = new LSR(cpu);
            this.eor = new EOR(cpu);
        }

        [NMOSOnly]
        [CPUInstruction(0x47, 5), ZeroPage]
        [CPUInstruction(0x57, 6), ZeroPageX]
        [CPUInstruction(0x4F, 6), Absolute]
        [CPUInstruction(0x5F, 7), AbsoluteX]
        [CPUInstruction(0x5B, 7), AbsoluteY]
        [CPUInstruction(0x43, 8), IndirectX]
        [CPUInstruction(0x53, 8), IndirectY]
        public void SRE_Memory(int address)
        {
            lsr.LSR_Memory(address);
            eor.EOR_Memory(address);
        }
    }
}