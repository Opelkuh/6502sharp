using System.Timers;

namespace AsmConsole
{
    class Program
    {
        private static MenuItem[] roms =
        {
            new MenuItem("Snake", "snake.bin"),
            new MenuItem("Calculator", "calculator.bin"),
            new MenuItem("Alive", "alive.bin"),
            new MenuItem("Brick", "brick.bin"),
            new MenuItem("Colors", "colors.bin"),
            new MenuItem("Compo-1st", "compo-1st.bin")
        };

        static void Main(string[] args)
        {
            GameConsole cons = new GameConsole();
            Menu menu = new Menu(roms);

            while (true)
            {
                var selected = menu.Start();

                cons.StartGame(selected.value, true);
            }
        }
    }
}
