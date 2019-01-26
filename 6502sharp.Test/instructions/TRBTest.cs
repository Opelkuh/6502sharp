using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class TRBTest : MachineCMOSBase
    {
        TRB _trb;

        public TRBTest()
        {
            _trb = new TRB(machine.CPU);
        }

        [Theory]
        [InlineData(0x33, 0xA6, 0x84)]
        [InlineData(0x41, 0xA6, 0xA6)]
        [InlineData(0x7F, 0x81, 0x80)]
        [InlineData(0xFF, 0x69, 0x00)]
        [InlineData(0xFF, 0x00, 0x00)]
        public void ShiftsMemory(byte accu, byte value, byte expected)
        {
            int memAddress = 0xABBA;

            machine.CPU.A.Value = accu;
            machine.Memory.Set(memAddress, value);

            _trb.TRB_Memory(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }

        [Theory]
        [InlineData(0x33, 0xA6, false)]
        [InlineData(0x41, 0xA6, true)]
        [InlineData(0x7F, 0x81, false)]
        [InlineData(0xFF, 0x69, false)]
        [InlineData(0xFF, 0x00, true)]
        public void SetsCorrectFlags(byte accu, byte value, bool expZ)
        {
            int memAddress = 0xABBA;
            
            machine.CPU.A.Value = accu;
            machine.Memory.Set(memAddress, value);

            _trb.TRB_Memory(memAddress);

            AssertFlag.Zero(machine, expZ);
        }
    }
}
