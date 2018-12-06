using System;
using System.IO;
using System.Timers;

namespace _6502sharp.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            Start("snake.bin", true);
        }

        private static void Start(string rom, bool loop = false)
        {
            ConsoleRenderer renderer = new ConsoleRenderer(32, 32);
            GameMemory mem = new GameMemory(renderer);

            IMachine mach = new NMOSMachine(mem);

            LoadRom(mach, $"./roms/{rom}", 0x0600);

            while (true)
            {   
                // input poll
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressed = Console.ReadKey(true);

                    // exit if escape
                    if(pressed.Key == ConsoleKey.Escape) break;

                    mach.Memory.Set(0xFF, (byte)pressed.KeyChar);
                }

                mach.CPU.Tick();

                if (loop && mach.CPU.PC.Value == 0) {
                    Start(rom, loop);
                    break;
                }
            }
        }

        private static void LoadRom(IMachine machine, string path, int offset)
        {
            byte[] data = File.ReadAllBytes(path);

            for (int i = 0; i < data.Length; i++)
            {
                machine.Memory.Set(i + offset, data[i]);
            }

            machine.CPU.PC.Value = (ushort)offset;
        }
    }
}
