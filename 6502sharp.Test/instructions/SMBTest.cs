using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class SMBTest : MachineCMOSBase
    {
        SMB _smb;

        public SMBTest()
        {
            _smb = new SMB(machine.CPU);
        }

        [Theory]
        [InlineData(0x01, 0x01)]
        [InlineData(0xFF, 0xFF)]
        [InlineData(0x6B, 0x6B)]
        [InlineData(0xCC, 0xCD)]
        [InlineData(0xF8, 0xF9)]
        [InlineData(0x4E, 0x4F)]
        public void SetsBit(byte value, byte expected)
        {
            int memAddress = 0x01;

            machine.Memory.Set(memAddress, value);

            _smb.SMB0(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }
    }
}
