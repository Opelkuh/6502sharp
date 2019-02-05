using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SHX : InstructionBase
    {
        public SHX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x9E, 5), Absolute]
        public void SHX_MemoryY(int address)
        {
            int value = (address >> 8) + 1;
            int res = cpu.X.Value & value;

            int target = address + cpu.Y.Value;
            cpu.Memory.Set(target, (byte)res);
        }
    }
}