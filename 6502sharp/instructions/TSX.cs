using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TSX
    {
        private ICpu _cpu;

        public TSX(ICpu cpu)
        {
            _cpu = cpu;
        }
        
        [CPUInstruction(0xBA, 2)]
        public void TSX_Implied()
        {
            _cpu.X.Value = _cpu.SP.Value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.X.Value);
        }
    }
}