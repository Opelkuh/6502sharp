using System;

namespace _6502sharp.Debug
{
    class GameMemory : Memory
    {
        private static readonly ConsoleColor[] COLORS = {
           ConsoleColor.Black,
           ConsoleColor.White,
           ConsoleColor.Red,
           ConsoleColor.Cyan,
           ConsoleColor.DarkMagenta,
           ConsoleColor.Green,
           ConsoleColor.Blue,
           ConsoleColor.Yellow,
           ConsoleColor.DarkRed,
           ConsoleColor.DarkYellow,
           ConsoleColor.Red,
           ConsoleColor.Black,
           ConsoleColor.Gray,
           ConsoleColor.Green,
           ConsoleColor.Blue,
           ConsoleColor.White,
        };

        private ConsoleRenderer renderer;
        private Random rng = new Random();

        public GameMemory(ConsoleRenderer renderer) : base(65536)
        {
            this.renderer = renderer;
        }

        public override byte Get(int location)
        {
            if (location == 0xFE)
            {
                return (byte)rng.Next();
            }
            else return base.Get(location);
        }

        public override void Set(int location, byte value)
        {
            if (location >= 0x0200 && location <= 0x05FF)
            {
                // get position
                int pixelNum = location - 0x0200;

                int x = pixelNum % 32;
                int y = pixelNum / 32;

                // get color
                ConsoleColor color = COLORS[value & 0x0F];

                // render
                renderer.SetPixel(x, y, color);
            }

            base.Set(location, value);
        }
    }
}
