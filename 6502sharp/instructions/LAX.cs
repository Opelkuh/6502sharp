using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class LAX : InstructionBase
    {
        public LAX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xA7, 3), ZeroPage]
        [CPUInstruction(0xB7, 4), ZeroPageY]
        [CPUInstruction(0xAF, 4), Absolute]
        [CPUInstruction(0xBF, 4), AbsoluteY]
        [CPUInstruction(0xA3, 6), IndirectX]
        [CPUInstruction(0xB3, 5), IndirectY]
        public void LAX_Memory(int address)
        {
            byte val = cpu.Memory.Get(address);

            cpu.A.Value = val;
            cpu.X.Value = val;

            flags.SetNegativeAndZero(val);
        }
    }
}