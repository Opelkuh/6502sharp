namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class BEQ
    {
        private ICpu _cpu;

        public BEQ(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xF0, 2)]
        public void BEQ_Relative([Relative] int target)
        {
            if (_cpu.SR.Zero == true)
            {
                _cpu.PC.Value = (ushort)(target & 0xFFFF);
            }
        }
    }
}