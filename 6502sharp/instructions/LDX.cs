using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class LDX : InstructionBase
    {
        public LDX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xA2, 2)]
        public void LDX_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xA6, 3), ZeroPage]
        [CPUInstruction(0xB6, 4), ZeroPageY]
        [CPUInstruction(0xAE, 4), Absolute]
        [CPUInstruction(0xBE, 4), AbsoluteY]
        public void LDX_Memory(int address)
        {
            process(cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            cpu.X.Value = value;

            FlagHelper.SetNegativeAndZero(cpu, cpu.X.Value);
        }
    }
}