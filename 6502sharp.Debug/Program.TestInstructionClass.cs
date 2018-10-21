using System;

namespace _6502sharp.Debug
{
    partial class Program
    {
        [InjectableInstruction]
        class TestInstructionClass
        {
            public TestInstructionClass(IMachine machine)
            {
                machine.PC.Value = 32000;
                Console.WriteLine(machine.PC.Value);

                machine.Memory.Set(10000, 69);
                Console.WriteLine(machine.Memory.Get(10000));
            }

            [CPUInstruction(0x69, 2)]
            public void SET_Abs([AbsoluteAdress] int memAddress, byte param) { }
        }
    }
}
