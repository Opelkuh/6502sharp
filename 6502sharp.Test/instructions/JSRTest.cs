using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class JSRTest : MachineNMOSBase
    {
        JSR _jsr;

        public JSRTest()
        {
            _jsr = new JSR(machine.CPU);
        }

        [Fact]
        public void PushesPCAndSetsPCToMemAddress()
        {
            int originalPc = 0xCAAC;
            int memAddress = 0xABBA;

            machine.CPU.PC.Value = originalPc;

            _jsr.JSR_Memory(memAddress);

            int stackValue = machine.CPU.Stack.PopPC();

            Assert.Equal(originalPc - 1, stackValue);
            Assert.Equal(memAddress, machine.CPU.PC.Value);
        }
    }
}
