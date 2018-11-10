using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class INCTest : MachineCMOSBase
    {
        INC _inc;

        public INCTest()
        {
            _inc = new INC(machine.CPU);
        }

        [Theory]
        [InlineData(0x5F, 0x60)]
        [InlineData(0x10, 0x11)]
        [InlineData(0xFF, 0x00)]
        public void IncrementsAccumulator(byte accu, byte expected)
        {
            machine.CPU.A.Value = accu;

            _inc.INC_Accumulator();

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x5F, 0x60)]
        [InlineData(0x10, 0x11)]
        [InlineData(0xFF, 0x00)]
        public void IncrementsMemory(byte value, byte expected)
        {
            int memAddress = 0xABBA;

            machine.Memory.Set(memAddress, value);

            _inc.INC_Memory(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }

        [Theory]
        [InlineData(0x7F, true, false)]
        [InlineData(0xFF, false, true)]
        public void SetsCorrectFlagsAccumulator(byte accu, bool expN, bool expZ)
        {
            machine.CPU.A.Value = accu;

            _inc.INC_Accumulator();

            AssertFlag.Negative(machine, expN);
            AssertFlag.Zero(machine, expZ);
        }

        [Theory]
        [InlineData(0x7F, true, false)]
        [InlineData(0xFF, false, true)]
        public void SetsCorrectFlagsMemory(byte value, bool expN, bool expZ)
        {
            int memAddress = 0xABBA;

            machine.Memory.Set(memAddress, value);

            _inc.INC_Memory(memAddress);

            AssertFlag.Negative(machine, expN);
            AssertFlag.Zero(machine, expZ);
        }
    }
}
