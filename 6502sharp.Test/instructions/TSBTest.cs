using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class TSBTest : MachineCMOSBase
    {
        TSB _tsb;

        public TSBTest()
        {
            _tsb = new TSB(machine.CPU);
        }

        [Theory]
        [InlineData(0x33, 0xA6, 0xB7)]
        [InlineData(0x41, 0xA6, 0xE7)]
        [InlineData(0x24, 0x18, 0x3C)]
        [InlineData(0xFF, 0x69, 0xFF)]
        [InlineData(0xFF, 0x00, 0xFF)]
        public void TestsAndSetsBits(byte accu, byte value, byte expected)
        {
            int memAddress = 0xABBA;

            machine.CPU.A.Value = accu;
            machine.Memory.Set(memAddress, value);

            _tsb.TSB_Memory(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }

        [Theory]
        [InlineData(0x33, 0xA6, false)]
        [InlineData(0x41, 0xA6, true)]
        [InlineData(0x24, 0x18, true)]
        [InlineData(0xFF, 0x69, false)]
        [InlineData(0xFF, 0x00, true)]
        public void SetsCorrectFlags(byte accu, byte value, bool expZ)
        {
            int memAddress = 0xABBA;
            
            machine.CPU.A.Value = accu;
            machine.Memory.Set(memAddress, value);

            _tsb.TSB_Memory(memAddress);

            AssertFlag.Zero(machine, expZ);
        }
    }
}
