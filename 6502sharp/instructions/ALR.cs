using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ALR : InstructionBase
    {
        public ALR(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0x4B, 2)]
        public void ALR_Immediate(byte value)
        {
            int and = cpu.A.Value & value;
            int res = (and << 1) | (cpu.SR.Carry ? 1 : 0);

            cpu.A.Value = (byte)res;

            cpu.SR.Carry = (and & (1 << 7)) > 0;
            flags.SetNegativeAndZero(res);
        }
    }
}