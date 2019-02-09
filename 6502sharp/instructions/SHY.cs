using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SHY : InstructionBase
    {
        public SHY(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0x9C, 5)]
        public void SHY_MemoryY([Absolute] int address)
        {
            int value = (address >> 8) + 1;
            int res = cpu.Y.Value & value;

            int target = address + cpu.X.Value;
            cpu.Memory.Set(target, (byte)res);
        }
    }
}