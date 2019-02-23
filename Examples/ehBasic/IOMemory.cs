using _6502sharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ehBasic
{
    class IOMemory : Memory
    {
        private const int READ_ADDR = 0xF004;
        private const int WRITE_ADDR = 0xF001;

        public bool Lock = false;
        public List<byte> Input = new List<byte>();

        public IOMemory() : base(65536)
        {
        }

        public override byte Get(int location)
        {
            if (location == READ_ADDR)
            {
                if (Input.Count > 0) return Input.Shift();
                else return 0;
            }

            return base.Get(location);
        }

        public override void Set(int location, byte value)
        {
            if (location == WRITE_ADDR)
            {
                Console.Write((char)value);
                return;
            }

            // prevent ROM overwritting
            if (Lock && location >= 0xC000) return;


            base.Set(location, value);
        }
    }
}
