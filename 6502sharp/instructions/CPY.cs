using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CPY : InstructionBase
    {
        public CPY(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xC0, 2)]
        public void CPY_Immediate(byte param)
        {
            process(param);
        }

        [CPUInstruction(0xC4, 3), ZeroPage]
        [CPUInstruction(0xCC, 4), Absolute]
        public void CPY_Memory(int address)
        {
            process(cpu.Memory.Get(address));
        }

        private void process(byte value)
        {
            compare.RegisterAndValue(cpu.Y.Value, value);
        }
    }
}