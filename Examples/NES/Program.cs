﻿using System;

namespace NES
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new GameWindow().Run(60);
        }
    }
}
