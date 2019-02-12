using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class LDY : InstructionBase
    {
        public LDY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xA0, 2)]
        public void LDY_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xA4, 3), ZeroPage]
        [CPUInstruction(0xB4, 4), ZeroPageX]
        [CPUInstruction(0xAC, 4), Absolute]
        [CPUInstruction(0xBC, 4), AbsoluteX]
        public void LDY_Memory(int address)
        {
            process(cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            cpu.Y.Value = value;

            flags.SetNegativeAndZero(cpu.Y.Value);
        }
    }
}