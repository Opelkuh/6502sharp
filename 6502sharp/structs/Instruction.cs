using System;

namespace _6502sharp
{
    public struct Instruction
        {
            public byte OpCode;
            public int Cycles;
            public Action Delegate;

            public Instruction(byte opcode, int cycles, Action instructionDelegate)
            {
                OpCode = opcode;
                Cycles = cycles;
                Delegate = instructionDelegate;
            }
        }
}