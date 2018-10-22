namespace _6502sharp
{
    public partial class CPU
    {
        protected struct Instruction
        {
            public byte OpCode;
            public int Cycles;
            public InstructionDelegate Delegate;

            public Instruction(byte opcode, int cycles, InstructionDelegate instructionDelegate)
            {
                OpCode = opcode;
                Cycles = cycles;
                Delegate = instructionDelegate;
            }
        }
    }
}