using System;
using System.Reflection;

namespace _6502sharp
{
    public class DefaultMachine : IMachine
    {
        public IReadable Memory => _ram;

        public ICpu CPU => _cpu;

        protected CPU _cpu;
        protected IReadable _ram;

        public DefaultMachine(CPUType type) : this(new Memory(65536), type)
        {
        }

        public DefaultMachine(IReadable memory, CPUType type)
        {
            _ram = memory;
            _cpu = new CPU(this, type);
        }
    }
}
