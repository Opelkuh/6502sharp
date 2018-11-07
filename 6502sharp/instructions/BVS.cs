namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class BVS
    {
        private ICpu _cpu;

        public BVS(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x70, 2)]
        public void BVS_Relative([Relative] int target)
        {
            if (_cpu.SR.Overflow == true) _cpu.PC.Value = target;
        }
    }
}