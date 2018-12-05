using System;

namespace _6502sharp
{
    public class InstructionEventArgs : EventArgs
    {
        public byte OpCode { get; set; }
        public int Cycles { get; set; }
    }
}