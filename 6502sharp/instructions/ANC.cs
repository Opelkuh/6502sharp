using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ANC : InstructionBase
    {
        public ANC(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0x0B, 2)]
        [CPUInstruction(0x2B, 2)]
        public void ANC_Immediate(byte value)
        {
            int res = cpu.A.Value & value;

            flags.SetNegativeAndZero(res);
            if ((res & 0x80) > 0) cpu.SR.Carry = true;
        }
    }
}