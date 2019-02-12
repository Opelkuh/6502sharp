using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class STY : InstructionBase
    {
        public STY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x84, 3), ZeroPage]
        [CPUInstruction(0x94, 4), ZeroPageX]
        [CPUInstruction(0x8C, 4), Absolute]
        public void STY_Memory(int address)
        {
            cpu.Memory.Set(address, cpu.Y.Value);
        }
    }
}