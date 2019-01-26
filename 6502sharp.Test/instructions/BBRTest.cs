using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class BBRTest : MachineCMOSBase
    {
        BBR _bbr;

        const int ZP_ADDR = 0x01;
        const int REL_ADDR = 0xAB;

        public BBRTest()
        {
            _bbr = new BBR(machine.CPU);
        }

        [Theory]
        [InlineData(0x01, false)]
        [InlineData(0xFF, false)]
        [InlineData(0x6B, false)]
        [InlineData(0xCC, true)]
        [InlineData(0xF8, true)]
        [InlineData(0x4E, true)]
        public void BranchesOnBitClear(byte value, bool shouldBranch)
        {
            machine.Memory.Set(ZP_ADDR, value);

            _bbr.BBR0(ZP_ADDR, REL_ADDR);

            Assert.Equal(shouldBranch, machine.CPU.PC.Value == REL_ADDR);
        }
    }
}
