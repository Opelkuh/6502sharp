using System;

namespace _6502sharp.Debug
{
    partial class Program
    {
        [InjectableInstruction]
        class TestInstructionClass
        {
            private ICpu _cpu;

            public TestInstructionClass(ICpu cpu)
            {
                _cpu = cpu;
                cpu.PC.Value = 32000;
                Console.WriteLine(cpu.PC.Value);

                cpu.Memory.Set(10000, 69);
                Console.WriteLine(cpu.Memory.Get(10000));
            }

            [CPUInstruction(0x69, 2)]
            public void SET_Abs(byte param, [AbsoluteAdress] int memAddress, byte param2)
            {
                Console.WriteLine($"PC: {_cpu.PC.Value}");
                Console.WriteLine($"Arg1: {param}");
                Console.WriteLine($"Mem address: {memAddress}");
                Console.WriteLine($"Arg2: {param2}");
                Console.WriteLine($"PC after: {_cpu.PC.Value}");
            }
        }
    }
}
