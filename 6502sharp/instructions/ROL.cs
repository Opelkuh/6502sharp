using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ROL : InstructionBase
    {
        public ROL(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x2A, 2)]
        public void ROL_Accumulator()
        {
            cpu.A.Value = process(cpu.A.Value);
        }

        [CPUInstruction(0x26, 5), ZeroPage]
        [CPUInstruction(0x36, 6), ZeroPageX]
        [CPUInstruction(0x2E, 6), Absolute]
        [CPUInstruction(0x3E, 7), AbsoluteX]
        public void ROL_Memory(int address)
        {
            byte rotated = process(cpu.Memory.Get(address));

            cpu.Memory.Set(address, rotated);
        }

        private byte process(byte value)
        {
            int res = (value << 1) | (cpu.SR.Carry ? 1 : 0);
            bool newCarry = (value & (1 << 7)) > 0;

            cpu.SR.Carry = newCarry;
            FlagHelper.SetNegativeAndZero(cpu, res);

            return (byte)res;
        }
    }
}