using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SBX : InstructionBase
    {
        public SBX(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0xCB, 2)]
        public void SBX_Immediate(byte value)
        {
            int and = cpu.X.Value & cpu.A.Value;
            int sub = and - value;
            byte res = (byte)sub;

            cpu.X.Value = res;

            // set flags
            cpu.SR.Carry = sub >= 0;
            flags.SetNegativeAndZero(res);
        }
    }
}