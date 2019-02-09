using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class LAS : InstructionBase
    {
        public LAS(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0xBB, 4), AbsoluteY]
        public void LAS_Memory(int address)
        {
            int and = cpu.SP.Value & cpu.Memory.Get(address);
            byte res = (byte)and;

            cpu.SP.Value = res;
            cpu.A.Value = res;
            cpu.X.Value = res;

            flags.SetNegativeAndZero(res);
        }
    }
}