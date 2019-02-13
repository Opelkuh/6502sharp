using System;

namespace AsmConsole
{
    class Menu
    {
        private MenuItem[] items;
        private int selected = 0;

        public Menu(params MenuItem[] items)
        {
            this.items = items;
        }

        public MenuItem Start()
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine();
                WriteCenter("SELECT ROM");
                Console.WriteLine();
                PrintOptions();

                var pressed = Console.ReadKey(true);
                switch (pressed.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selected--;
                        if (selected < 0) selected = items.Length - 1;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selected++;
                        selected %= items.Length;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        loop = false;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }

            return items[selected];
        }

        private void PrintOptions()
        {
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];

                if (i == selected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    WriteCenter($"> {item.displayValue} <");
                    Console.ResetColor();
                }
                else
                {
                    WriteCenter(item.displayValue);
                }
            }
        }

        private void WriteCenter(string s)
        {
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
        }
    }
}
