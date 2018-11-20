namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class CLC
    {
        private ICpu _cpu;

        public CLC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x18, 2)]
        public void CLC_Implied()
        {
            _cpu.SR.Carry = false;
        }
    }
}