using System;
using System.IO;

namespace _6502sharp.Debug
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Memory mem = new Memory(65536);

            IMachine mach = new NMOSMachine();

            LoadTestRom(mach, @"../AllSuiteA.bin", 0x4000);

            mach.CPU.PC.Value = 0x4000;

            while(mach.CPU.PC.Value < 0x45C0) {
                mach.CPU.Tick();
            }

            Console.WriteLine(mach.Memory.Get(0x0210));
        }

        private static void LoadTestRom(IMachine machine, string path, int offset)
        {
            byte[] data = File.ReadAllBytes(path);

            for (int i = 0; i < data.Length; i++)
            {
                machine.Memory.Set(i + offset, data[i]);
            }
        }
    }
}
