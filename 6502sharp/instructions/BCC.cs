namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class BCC
    {
        private ICpu _cpu;

        public BCC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x90, 2)]
        public void BCC_Relative([Relative] int target)
        {
            if (_cpu.SR.Carry == false) _cpu.PC.Value = target;
        }
    }
}