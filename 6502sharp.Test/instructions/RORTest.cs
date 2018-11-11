using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class RORTest : MachineNMOSBase
    {
        ROR _ror;

        public RORTest()
        {
            _ror = new ROR(machine.CPU);
        }

        [Theory]
        [InlineData(0x00, true, 0x80)]
        [InlineData(0x80, false, 0x40)]
        public void RollsLeftAccumulator(byte accu, bool carry, byte expected)
        {
            machine.CPU.A.Value = accu;
            machine.CPU.SR.Carry = carry;

            _ror.ROR_Accumulator();

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x00, true, 0x80)]
        [InlineData(0x80, false, 0x40)]
        public void RollsLeftMemory(byte value, bool carry, byte expected)
        {
            int memAddress = 0xABBA;

            machine.Memory.Set(memAddress, value);
            machine.CPU.SR.Carry = carry;

            _ror.ROR_Memory(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }

        [Theory]
        [InlineData(0x01, false, true, false, true)]
        [InlineData(0x01, true, true, true, false)]
        [InlineData(0x00, true, false, true, false)]
        [InlineData(0xFF, true, true, true, false)]
        public void SetsCorrectFlags(
            byte accu,
            bool carry,
            bool expC,
            bool expN,
            bool expZ
        )
        {
            machine.CPU.A.Value = accu;
            machine.CPU.SR.Carry = carry;

            _ror.ROR_Accumulator();

            AssertFlag.Carry(machine, expC);
            AssertFlag.Negative(machine, expN);
            AssertFlag.Zero(machine, expZ);
        }
    }
}
