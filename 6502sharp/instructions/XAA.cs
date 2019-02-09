using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class XAA : InstructionBase
    {
        public XAA(ICpu cpu) : base(cpu)
        {
        }

        /*
        *
        * This instruction has no exact definition. You'll probably want to change this
        * instruction depending on the machine you're trying to emulate.
        * 
        * This emulator uses the definition found here:
        *     http://visual6502.org/wiki/index.php?title=6502_Opcode_8B_%28XAA,_ANE%29
        * 
        * Magic value used: 0xEE
        *
        */
        [CPUInstruction(0x8B, 2)]
        public void XAA_Immediate(byte value)
        {
            int res = cpu.X.Value & value & (cpu.A.Value & 0xEE);

            cpu.A.Value = (byte)res;
        }
    }
}