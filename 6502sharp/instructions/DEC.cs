using _6502sharp.Helpers;

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

            FlagHelper.SetNegative(cpu, cpu.A.Value);
            FlagHelper.SetZero(cpu, cpu.A.Value);
        }

        [CPUInstruction(0xC6, 5), ZeroPage]
        [CPUInstruction(0xD6, 6), ZeroPageX]
        [CPUInstruction(0xCE, 6), Absolute]
        [CPUInstruction(0xDE, 7), AbsoluteX]
        public void DEC_Memory(int address)
        {
            byte val = cpu.Memory.Get(address);

            val--;

            FlagHelper.SetNegativeAndZero(cpu, val);

            cpu.Memory.Set(address, val);
        }
    }
}