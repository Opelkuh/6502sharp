using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class DEC : InstructionBase
    {
        public DEC(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x3A, 2, CPUType.CMOS)]
        public void DEC_Accumulator()
        {
            cpu.A.Value--;

            flags.SetNegativeAndZero(cpu.A.Value);
        }

        [CPUInstruction(0xC6, 5), ZeroPage]
        [CPUInstruction(0xD6, 6), ZeroPageX]
        [CPUInstruction(0xCE, 6), Absolute]
        [CPUInstruction(0xDE, 7), AbsoluteX]
        public void DEC_Memory(int address)
        {
            byte val = cpu.Memory.Get(address);

            val--;

            flags.SetNegativeAndZero(val);

            cpu.Memory.Set(address, val);
        }
    }
}