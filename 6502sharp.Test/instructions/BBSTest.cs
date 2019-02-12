using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class BBSTest : MachineCMOSBase
    {
        BBS _bbs;

        const int ZP_ADDR = 0x01;
        const int REL_ADDR = 0xAB;

        public BBSTest()
        {
            _bbs = new BBS(machine.CPU);
        }

        [Theory]
        [InlineData(0x01, true)]
        [InlineData(0xFF, true)]
        [InlineData(0x6B, true)]
        [InlineData(0xCC, false)]
        [InlineData(0xF8, false)]
        [InlineData(0x4E, false)]
        public void BranchesOnBitClear(byte value, bool shouldBranch)
        {
            machine.Memory.Set(ZP_ADDR, value);

            _bbs.BBS0(ZP_ADDR, REL_ADDR);

            Assert.Equal(shouldBranch, machine.CPU.PC.Value == REL_ADDR);
        }
    }
}
