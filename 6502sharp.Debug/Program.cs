using System;

namespace _6502sharp.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Memory mem = new Memory(65535);
            mem.SetEvent += SetEventTest;

            mem.Set(0, 255);

            Console.WriteLine("Memory position 0x0000: " + mem.Get(0));
        }

        private static bool SetEventTest(ref int location, in byte oldValue, ref byte newValue) {
            newValue += 5;
            return true;
        }
    }
}
