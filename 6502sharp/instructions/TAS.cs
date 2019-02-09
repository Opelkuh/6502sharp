using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TAS : InstructionBase
    {
        public TAS(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0x9B, 5)]
        public void TAS_MemoryY([Absolute] int address)
        {
            int and = cpu.A.Value & cpu.X.Value;

            cpu.SP.Value = (byte)and;

            int addrHi = address >> 8;
            int res = and & (addrHi + 1);

            cpu.Memory.Set(address + cpu.Y.Value, (byte)res);
        }
    }
}