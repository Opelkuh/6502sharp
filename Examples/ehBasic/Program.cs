using System;
using _6502sharp;

namespace ehBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            IOMemory memory = new ehBasic.IOMemory();
            NMOSMachine mach = new NMOSMachine(memory);

            mach.LoadRom("ehbasic.bin", 0xC000);

            memory.Lock = true;

            mach.CPU.Reset();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressed = Console.ReadKey(true);

                    // exit if escape
                    if (pressed.Key == ConsoleKey.Escape) break;

                    memory.Input.Add((byte)pressed.KeyChar);
                }

                mach.CPU.NextTick();
            }
        }
    }
}
