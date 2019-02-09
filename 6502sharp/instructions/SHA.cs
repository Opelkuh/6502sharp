using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SHA : InstructionBase
    {
        public SHA(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0x9F, 5), Absolute]
        [CPUInstruction(0x93, 6), Indirect]
        public void SHA_MemoryY(int address)
        {
            int value = (address >> 8) + 1;
            int res = cpu.A.Value & cpu.X.Value & value;

            int target = address + cpu.Y.Value;
            cpu.Memory.Set(target, (byte)res);
        }
    }
}