namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BCS
    {
        private ICpu _cpu;

        public BCS(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0xB0, 2)]
        public void BCS_Relative([Relative] int target)
        {
            if (_cpu.SR.Carry == true) _cpu.PC.Value = target;
        }
    }
}