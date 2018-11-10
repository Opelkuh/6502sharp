using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class ORATest : MachineNMOSBase
    {
        ORA _ora;

        public ORATest()
        {
            _ora = new ORA(machine.CPU);
        }

        [Theory]
        [InlineData(0xAA, 0x55, 0xFF)]
        [InlineData(0x00, 0x69, 0x69)]
        public void ORsImmediate(byte accu, byte value, byte expected)
        {
            machine.CPU.A.Value = accu;

            _ora.ORA_Immediate(value);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0xAA, 0x55, 0xFF)]
        [InlineData(0x00, 0x69, 0x69)]
        public void ORsMemory(byte accu, byte value, byte expected)
        {
            int memAddress = 0xABBA;

            machine.Memory.Set(memAddress, value);
            machine.CPU.A.Value = accu;

            _ora.ORA_Memory(memAddress);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x0F, 0x80, true, false)]
        [InlineData(0x00, 0x00, false, true)]
        public void SetsCorrectFlags(byte accu, byte value, bool expN, bool expZ)
        {
            machine.CPU.A.Value = accu;

            _ora.ORA_Immediate(value);

            AssertFlag.Negative(machine, expN);
            AssertFlag.Zero(machine, expZ);
        }
    }
}
