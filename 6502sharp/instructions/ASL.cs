using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ASL : InstructionBase
    {
        public ASL(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x0A, 2)]
        public void ASL_Accumulator()
        {
            cpu.A.Value = process(cpu.A.Value);
        }

        [CPUInstruction(0x06, 5), ZeroPage]
        [CPUInstruction(0x16, 6), ZeroPageX]
        [CPUInstruction(0x0E, 6), Absolute]
        [CPUInstruction(0x1E, 7), AbsoluteX]
        public void ASL_Memory(int address)
        {
            byte value = cpu.Memory.Get(address);
            byte shifted = process(value);

            cpu.Memory.Set(address, shifted);
        }

        private byte process(byte value)
        {
            int shifted = value << 1;

            FlagHelper.SetCarry(cpu, shifted);
            FlagHelper.SetZero(cpu, shifted);
            FlagHelper.SetNegative(cpu, shifted);

            return (byte)(shifted);
        }
    }
}