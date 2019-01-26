using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class RMBTest : MachineCMOSBase
    {
        RMB _rmb;

        public RMBTest()
        {
            _rmb = new RMB(machine.CPU);
        }

        [Theory]
        [InlineData(0x01, 0x00)]
        [InlineData(0xFF, 0xFE)]
        [InlineData(0x6B, 0x6A)]
        [InlineData(0xCC, 0xCC)]
        [InlineData(0xF8, 0xF8)]
        [InlineData(0x4E, 0x4E)]
        public void ResetsBit(byte value, byte expected)
        {
            int memAddress = 0x01;

            machine.Memory.Set(memAddress, value);

            _rmb.RMB0(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }
    }
}
