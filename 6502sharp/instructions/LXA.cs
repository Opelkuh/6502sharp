using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class LXA : InstructionBase
    {
        public LXA(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0xAB, 2)]
        public void LXA_Immediate(byte value)
        {
            int and = cpu.A.Value & value;
            byte res = (byte)and;

            cpu.A.Value = res;
            cpu.X.Value = res;

            flags.SetNegativeAndZero(res);
        }
    }
}