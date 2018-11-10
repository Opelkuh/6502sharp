using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class JMPTest : MachineCMOSBase
    {
        JMP _jmp;

        public JMPTest()
        {
            _jmp = new JMP(machine.CPU);
        }

        [Fact]
        public void SetsPCToMemAddress()
        {
            int memAddress = 0xABBA;

            _jmp.JMP_Memory(memAddress);

            Assert.Equal(memAddress, machine.CPU.PC.Value);
        }
    }
}
