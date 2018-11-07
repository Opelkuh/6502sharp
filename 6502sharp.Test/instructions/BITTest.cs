using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class BITTest : MachineNMOSBase
    {
        BIT _bit;

        public BITTest()
        {
            _bit = new BIT(machine.CPU);
        }

        [Theory]
        [InlineData(0x05, 0x00, true, false, false)]
        [InlineData(0x00, 0x7F, true, false, true)]
        [InlineData(0xAB, 0xC1, false, true, true)]
        public void SetsFlags(
            int accu,
            int value,
            bool expZ,
            bool expN,
            bool expV
        )
        {
            int memAddress = 0x69;

            machine.CPU.A.Value = (byte)accu;
            machine.Memory.Set(memAddress, (byte)value);

            _bit.BIT_Memory(memAddress);

            AssertFlag.Zero(machine, expZ);
            AssertFlag.Negative(machine, expN);
            AssertFlag.Overflow(machine, expV);
        }

        [Fact]
        public void SetsFlagsImmediate() {
            machine.CPU.A.Value = 0xAB;

            _bit.BIT_Immediate(0xC1);

            AssertFlag.Zero(machine, false);
            AssertFlag.Negative(machine, true);
            AssertFlag.Overflow(machine, true);
        }
    }
}
