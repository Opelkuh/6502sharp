using System;
using System.Reflection;

namespace _6502sharp
{
    public partial class DefaultMachine : IMachine
    {
        public EventReadableBase Memory => _ram;
        public ICpu CPU => _cpu;

        private ICpu _cpu;
        private Memory _ram;

        public DefaultMachine() : this(new Memory(65536))
        {
        }

        public DefaultMachine(Memory memory)
        {
            _ram = memory;
            _cpu = new CPU(this);
        }
    }
}
