using System;
using NES.PPU;
using NES.Rendering;

namespace NES
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            ColorPalette.Initialize();

            new GameWindow().Run(60);
        }
    }
}
