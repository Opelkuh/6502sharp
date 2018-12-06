using System;

namespace _6502sharp.Debug
{
    class ConsoleRenderer
    {
        private int width;
        private int height;

        public ConsoleRenderer(int width, int height)
        {
            this.width = width;
            this.height = height;
            
            Console.Clear();
        }

        public void SetPixel(int x, int y, ConsoleColor color)
        {
            Console.BackgroundColor = color;

            Console.SetCursorPosition(x, y);
            Console.Write(" ");
            Console.SetCursorPosition(width - 1, height - 1);

            Console.BackgroundColor = ConsoleColor.Black;

        }
    }
}
