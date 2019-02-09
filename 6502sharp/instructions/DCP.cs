using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class DCP : InstructionBase
    {
        public DCP(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0xC7, 5), ZeroPage]
        [CPUInstruction(0xD7, 6), ZeroPageX]
        [CPUInstruction(0xCF, 6), Absolute]
        [CPUInstruction(0xDF, 7), AbsoluteX]
        [CPUInstruction(0xDB, 7), AbsoluteY]
        [CPUInstruction(0xC3, 8), IndirectX]
        [CPUInstruction(0xD3, 8), IndirectY]
        public void DCP_Memory(int address)
        {
            int res = cpu.Memory.Get(address) - 1;

            compare.RegisterAndValue(cpu.A.Value, (byte)res);
        }
    }
}