using System;
using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [InjectableInstruction]
    public class BRK
    {
        private ICpu _cpu;

        public BRK(ICpu cpu)
        {
            _cpu = cpu;
        }

        [CPUInstruction(0x00, 7)]
        public void BRK_Implied()
        {
            // save PC + 1
            _cpu.PC.Value++;
            _cpu.Stack.PushPC();

            // save status reg
            _cpu.SR.Break = true;
            _cpu.Stack.Push(_cpu.SR.Value);
            _cpu.SR.Break = false;

            // set interrupt flag
            _cpu.SR.Interrupt = true;

            // set new PC
            byte[] target = { _cpu.Memory.Get(0xFFFE), _cpu.Memory.Get(0xFFFF) };

            _cpu.PC.Value = LEHelper.From(target);
        }
    }
}