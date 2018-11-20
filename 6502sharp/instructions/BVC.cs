namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BVC
    {
        private ICpu _cpu;

        public BVC(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x50, 2)]
        public void BVC_Relative([Relative] int target)
        {
            if (_cpu.SR.Overflow == false) _cpu.PC.Value = target;
        }
    }
}