using System;
using System.IO;
using System.Reflection;
using _6502sharp.Helpers;

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

        /// <summary>
        /// Loads file contents into memory at specified location and loads the
        /// reset vector (0xFFFC and 0xFFFD) with start of the program.  
        /// </summary>
        /// <param name="path">path to binary</param>
        /// <param name="location">where to start writing and set reset vector to</param>
        public void LoadRom(string path, ushort location)
        {
            byte[] data = File.ReadAllBytes(path);
            LoadRom(data, location);
        }

        /// <summary>
        /// Loads bytes into memory at specified location and loads the reset
        /// vector (0xFFFC and 0xFFFD) with start of the program.  
        /// </summary>
        /// <param name="data"></param>
        /// <param name="location">where to start writing and set reset vector to</param>
        public void LoadRom(byte[] data, ushort location)
        {
            for (int i = 0; i < data.Length; i++)
            {
                _ram.Set(i + location, data[i]);
            }
        }
    }
}
