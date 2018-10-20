namespace _6502sharp.Debug
{
    partial class Program
    {
        [InjectableInstruction]
        class TestInstructionClass
        {
            public TestInstructionClass()
            {

            }

            [CPUInstruction(0x69, 2)]
            public void SET_Abs([AbsoluteAdress] int memAddress) { }
        }
    }
}
