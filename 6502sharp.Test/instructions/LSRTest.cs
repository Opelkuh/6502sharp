using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class LSRTest : MachineNMOSBase
    {
        LSR _lsr;

        public LSRTest()
        {
            _lsr = new LSR(machine.CPU);
        }

        [Theory]
        [InlineData(0x80, 0x40)]
        [InlineData(0xFF, 0x7F)]
        public void ShiftsAccumulator(byte accu, byte expected)
        {
            machine.CPU.A.Value = accu;

            _lsr.LSR_Accumulator();

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x80, 0x40)]
        [InlineData(0xFF, 0x7F)]
        public void ShiftsMemory(byte value, byte expected)
        {
            int memAddress = 0xABBA;

            machine.Memory.Set(memAddress, value);

            _lsr.LSR_Memory(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }

        [Theory]
        [InlineData(0xFD, true, false)]
        [InlineData(0xFE, false, false)]
        [InlineData(0x01, true, true)]
        public void SetsCorrectFlags(byte accu, bool expC, bool expZ)
        {
            machine.CPU.A.Value = accu;

            _lsr.LSR_Accumulator();

            AssertFlag.Carry(machine, expC);
            AssertFlag.Zero(machine, expZ);
            AssertFlag.Negative(machine, false);
        }
    }
}
