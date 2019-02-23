using System;
using _6502sharp;
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

            RomData? data = RomParser.ParseFile("bin/Debug/netcoreapp2.2/roms/donkeykong.nes");

            NESGame.Create(data.Value);
        }
    }
}