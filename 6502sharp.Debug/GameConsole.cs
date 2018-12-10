using System;
using System.IO;

namespace _6502sharp.Debug
{
    public class GameConsole
    {
        private IMachine machine;

        public void StartGame(string rom, bool loop = false)
        {
            ConsoleRenderer renderer = new ConsoleRenderer(32, 32);
            GameMemory mem = new GameMemory(renderer);

            machine = new NMOSMachine(mem);

            LoadRom($"./roms/{rom}", 0x0600);

            int slow = 0;
            while (true)
            {
                // input poll
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressed = Console.ReadKey(true);

                    // exit if escape
                    if (pressed.Key == ConsoleKey.Escape) break;

                    machine.Memory.Set(0xFF, (byte)pressed.KeyChar);
                }

                machine.CPU.Tick();

                if (loop && machine.CPU.PC.Value == 0)
                {
                    StartGame(rom, loop);
                    break;
                }
            }
        }

        private void LoadRom(string path, int offset)
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
