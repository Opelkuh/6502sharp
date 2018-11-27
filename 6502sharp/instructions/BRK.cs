using System;
using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BRK : InstructionBase
    {
        public BRK(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x00, 7)]
        public void BRK_Implied()
        {
            // save PC + 1
            cpu.PC.Value++;
            cpu.Stack.PushPC();

            // save status reg
            cpu.SR.Break = true;
            cpu.Stack.Push(cpu.SR.Value);
            cpu.SR.Break = false;

            // set interrupt flag
            cpu.SR.Interrupt = true;

            // set new PC
            byte pcLo = cpu.Memory.Get(0xFFFE);
            byte pcHi = cpu.Memory.Get(0xFFFF);

            cpu.PC.Set(0, pcLo);
            cpu.PC.Set(1, pcHi);
        }
    }
}