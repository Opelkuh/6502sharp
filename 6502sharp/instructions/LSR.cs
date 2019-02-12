using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class LSR : InstructionBase
    {
        public LSR(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x4A, 2)]
        public void LSR_Accumulator()
        {
            cpu.A.Value = process(cpu.A.Value);
        }

        [CPUInstruction(0x46, 5), ZeroPage]
        [CPUInstruction(0x56, 6), ZeroPageX]
        [CPUInstruction(0x4E, 6), Absolute]
        [CPUInstruction(0x5E, 7), AbsoluteX]
        public void LSR_Memory(int address)
        {
            byte shifted = process(cpu.Memory.Get(address));

            cpu.Memory.Set(address, shifted);
        }

        private byte process(byte value)
        {
            bool carry = (value & 0x01) > 0;
            int res = value >> 1;

            cpu.SR.Carry = carry;
            cpu.SR.Negative = false;
            flags.SetZero(res);

            return (byte)res;
        }
    }
}