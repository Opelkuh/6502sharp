using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class RTI
    {
        private ICpu _cpu;

        public RTI(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x40, 6)]
        public void RTI_Implied()
        {
            _cpu.SR.Value = _cpu.Stack.Pop();
            _cpu.PC.Value = _cpu.Stack.PopPC();
        }
    }
}