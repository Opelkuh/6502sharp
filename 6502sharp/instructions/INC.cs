using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class INC : InstructionBase
    {
        public INC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x1A, 2, CPUType.CMOS)]
        public void INC_Accumulator()
        {
            cpu.A.Value++;

            flags.SetNegativeAndZero(cpu.A.Value);
        }

        [CPUInstruction(0xE6, 5), ZeroPage]
        [CPUInstruction(0xF6, 6), ZeroPageX]
        [CPUInstruction(0xEE, 6), Absolute]
        [CPUInstruction(0xFE, 7), AbsoluteX]
        public void INC_Memory(int address)
        {
            byte val = cpu.Memory.Get(address);

            val++;

            flags.SetNegativeAndZero(val);

            cpu.Memory.Set(address, val);
        }
    }
}