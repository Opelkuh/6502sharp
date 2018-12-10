using System.Timers;

namespace _6502sharp.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            GameConsole cons = new GameConsole();

            cons.StartGame("snake.bin", true);
        }
    }
}
