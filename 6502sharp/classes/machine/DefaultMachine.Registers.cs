using System;
using System.Reflection;

namespace _6502sharp
{
    public partial class DefaultMachine : IMachine
    {
        public StatusRegister SR => _sr;

        public Register A => _a;

        public Register X => _x;

        public Register Y => _y;

        public Register SP => _sp;

        public Register16Bit PC => _pc;


        private StatusRegister _sr = new StatusRegister();
        private Register _a = new Register();
        private Register _x = new Register();
        private Register _y = new Register();
        private Register _sp = new Register();
        private Register16Bit _pc = new Register16Bit();
    }
}
