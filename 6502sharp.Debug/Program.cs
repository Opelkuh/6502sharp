using System;
using System.IO;
using System.Timers;

namespace _6502sharp.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            ConsoleRenderer renderer = new ConsoleRenderer(32, 32);
            GameMemory mem = new GameMemory(renderer);

            IMachine mach = new NMOSMachine(mem);

            // LoadRom(mach, "./roms/compo-1st.bin", 0x0600);
            LoadRom(mach, "./roms/snake.bin", 0x0600);

            DateTime nextTick = DateTime.Now;
            while (true)
            {
                if (DateTime.Now < nextTick) continue;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressed = Console.ReadKey(true);
                    mach.Memory.Set(0xFF, (byte)pressed.KeyChar);
                }

                mach.CPU.Tick();

                if (mach.CPU.PC.Value == 0) {
                    Start();
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
