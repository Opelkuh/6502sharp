using System;

namespace _6502sharp.Debug
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Memory mem = new Memory(65536);
            MemRandomFill(mem);

            IMachine mach = new DefaultMachine(mem);

            mach.CPU.Tick();
            mach.CPU.Tick();
            mach.CPU.Tick();
            
            // Test16BitReg();
            // TestStatusReg();
        }

        private static void MemRandomFill(Memory mem)
        {
            for (int i = 0; i < mem.Size; i++)
            {
                mem.Set(i, (byte)i);
            }
        }

        private static void TestStatusReg()
        {
            StatusRegister sr = new StatusRegister();

            sr.Carry = true;

            Console.WriteLine("Carry flag after true: " + sr.Carry);

            sr.Carry = false;

            Console.WriteLine("Carry flag after false: " + sr.Carry);

            Console.WriteLine("SR value: " + Convert.ToString(sr.Value, 2).PadLeft(8, '0'));
            sr.Negative = true;
            sr.Unused = true;
            sr.Decimal = true;
            sr.Zero = true;
            Console.WriteLine("SR value: " + Convert.ToString(sr.Value, 2).PadLeft(8, '0'));
        }

        private static void Test16BitReg()
        {
            Register16Bit pc = new Register16Bit();

            pc.Value = 30000;

            Console.WriteLine("PC value: " + pc.Value);
        }

        private static void TestMem()
        {
            Memory mem = new Memory(65535);

            mem.Set(0, 255);

            Console.WriteLine("Memory position 0x0000: " + mem.Get(0));
        }
    }
}
