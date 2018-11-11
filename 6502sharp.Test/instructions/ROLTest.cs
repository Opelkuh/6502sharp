using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class ROLTest : MachineNMOSBase
    {
        ROL _rol;

        public ROLTest()
        {
            _rol = new ROL(machine.CPU);
        }

        [Theory]
        [InlineData(0x80, false, 0x00)]
        [InlineData(0x40, true, 0x81)]
        public void RollsLeftAccumulator(byte accu, bool carry, byte expected)
        {
            machine.CPU.A.Value = accu;
            machine.CPU.SR.Carry = carry;

            _rol.ROL_Accumulator();

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x80, false, 0x00)]
        [InlineData(0x40, true, 0x81)]
        public void RollsLeftMemory(byte value, bool carry, byte expected)
        {
            int memAddress = 0xABBA;

            machine.Memory.Set(memAddress, value);
            machine.CPU.SR.Carry = carry;

            _rol.ROL_Memory(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }

        [Theory]
        [InlineData(0x80, false, true, false, true)]
        [InlineData(0x80, true, true, false, false)]
        [InlineData(0x40, false, false, true, false)]
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

            _rol.ROL_Accumulator();

            AssertFlag.Carry(machine, expC);
            AssertFlag.Negative(machine, expN);
            AssertFlag.Zero(machine, expZ);
        }
    }
}
