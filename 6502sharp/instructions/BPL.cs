namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BPL
    {
        private ICpu _cpu;

        public BPL(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x10, 2)]
        public void BPL_Relative([Relative] int target)
        {
            if (_cpu.SR.Negative == false) _cpu.PC.Value = target;
        }
    }
}