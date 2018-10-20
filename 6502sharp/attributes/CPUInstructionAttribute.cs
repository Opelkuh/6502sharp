using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CPUInstructionAttribute : Attribute
    {
        public byte OpCode;
        public int Cycles;

        public CPUInstructionAttribute(byte opcode, int cycles)
        {
            OpCode = opcode;
            Cycles = cycles;
        }
    }
}
