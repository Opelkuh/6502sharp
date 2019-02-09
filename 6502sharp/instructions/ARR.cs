using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class ARR : InstructionBase
    {
        public ARR(ICpu cpu) : base(cpu)
        {
        }

        [NMOSOnly]
        [CPUInstruction(0x6B, 2)]
        public void ARR_Immediate(byte value)
        {
            if (cpu.SR.Decimal && cpu.DecimalMode)
                processDecimal(value);
            else
                processBinary(value);
        }

        void processBinary(byte value)
        {
            int and = cpu.A.Value & value;

            int res = (and >> 1) | (cpu.SR.Carry ? 1 << 7 : 0);

            // set flags
            flags.SetNegativeAndZero(res);

            // carry is taken from bit 6
            cpu.SR.Carry = ((1 << 6) & res) > 0;
            // overflow is XOR of bit 6 and 5
            cpu.SR.Overflow = ((1 << 6) ^ (1 << 5)) > 0;
        }

        void processDecimal(byte value)
        {
            int and = cpu.A.Value & value;

            int res = (and >> 1) | (cpu.SR.Carry ? 1 << 7 : 0);

            // set flags
            flags.SetZero(res);
            // negative flag is taken from original carry
            cpu.SR.Negative = cpu.SR.Carry;
            // overflow flag is set if bit 6 changed between AND and ROR
            cpu.SR.Overflow = ((1 << 6) & and) != ((1 << 6) & res);

            // decimal mode adjust
            int andLo = and & 0x0F;
            int andHi = and >> 4;

            if ((andLo + andLo & 1) > 5) {
                res = res & 0xF0 | ((res + 6) & 0x0F);
            }

            if ((andHi + andHi & 1) > 5) {
                res += 6 << 4;
                res &= 0xFF;

                cpu.SR.Carry = true;
            }
            else cpu.SR.Carry = false;
        }
    }
}