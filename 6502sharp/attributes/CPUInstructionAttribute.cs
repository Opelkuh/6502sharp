using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CPUInstructionAttribute : Attribute
    {
        public byte OpCode;
        public int Cycles;
        public CPUType CPUType;

        public CPUInstructionAttribute(byte opcode, int cycles)
        {
            OpCode = opcode;
            Cycles = cycles;
            CPUType = CPUType.NMOS | CPUType.CMOS;
        }

        public CPUInstructionAttribute(byte opcode, int cycles, CPUType type) : this(opcode, cycles)
        {
            CPUType = type;
        }
    }
}
