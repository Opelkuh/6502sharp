using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ROR : InstructionBase
    {
        public ROR(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x6A, 2)]
        public void ROR_Accumulator()
        {
            cpu.A.Value = process(cpu.A.Value);
        }

        [CPUInstruction(0x66, 5), ZeroPage]
        [CPUInstruction(0x76, 6), ZeroPageX]
        [CPUInstruction(0x6E, 6), Absolute]
        [CPUInstruction(0x7E, 7), AbsoluteX]
        public void ROR_Memory(int address)
        {
            byte rotated = process(cpu.Memory.Get(address));

            cpu.Memory.Set(address, rotated);
        }

        private byte process(byte value)
        {
            int res = (value >> 1) | (cpu.SR.Carry ? 1 << 7 : 0);
            bool newCarry = (value & 0x01) > 0;

            cpu.SR.Carry = newCarry;
            FlagHelper.SetNegativeAndZero(cpu, res);

            return (byte)res;
        }
    }
}