namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class BMI
    {
        private ICpu _cpu;

        public BMI(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x30, 2)]
        public void BMI_Relative([Relative] int target)
        {
            if (_cpu.SR.Negative == true)
            {
                _cpu.PC.Value = (ushort)(target & 0xFFFF);
            }
        }
    }
}