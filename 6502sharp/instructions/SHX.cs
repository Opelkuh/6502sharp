using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SHX : InstructionBase
    {
        public SHX(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0x9E, 5)]
        public void SHX_MemoryY([Absolute] int address)
        {
            int value = (address >> 8) + 1;
            int res = cpu.X.Value & value;

            int target = address + cpu.Y.Value;
            cpu.Memory.Set(target, (byte)res);
        }
    }
}