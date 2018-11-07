namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class BNE
    {
        private ICpu _cpu;

        public BNE(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xD0, 2)]
        public void BNE_Relative([Relative] int target)
        {
            if (_cpu.SR.Zero == false)
            {
                _cpu.PC.Value = (ushort)(target & 0xFFFF);
            }
        }
    }
}