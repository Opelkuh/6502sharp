using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class TAY
    {
        private ICpu _cpu;

        public TAY(ICpu cpu)
        {
            _cpu = cpu;
        }
        
        [CPUInstruction(0xA8, 2)]
        public void TAY_Implied()
        {
            _cpu.Y.Value = _cpu.Y.Value;

            FlagHelper.SetNegativeAndZero(_cpu, _cpu.Y.Value);
        }
    }
}