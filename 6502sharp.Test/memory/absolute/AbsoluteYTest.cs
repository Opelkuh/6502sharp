using Xunit;

namespace _6502sharp.Test.Memory
{
    public class AbsoluteYTest : MachineNMOSBase
    {
        AbsoluteAddressYAttribute attr;

        public AbsoluteYTest() : base()
        {
            attr = new AbsoluteAddressYAttribute();
        }

        [Theory]
        [InlineData(0xFF, 0xEE, 0x0A, 61193, 1)]
        [InlineData(0x0F, 0xEE, 0x0A, 60953, 0)]
        public void ReturnsAddress(
            byte in1,
            byte in2,
            byte aValue,
            int expected,
            int sleepExpected
        )
        {
            byte[] input = { in1, in2 };
            machine.CPU.Y.Value = aValue;
            machine.CPU.SleepCycles = 0;

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(expected, res);
            Assert.Equal(sleepExpected, machine.CPU.SleepCycles);
        }
    }
}
