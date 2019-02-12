using Xunit;
using _6502sharp.Reflection;

namespace _6502sharp.Test.Memory
{
    public class ZeroPageYTest : MachineNMOSBase
    {
        ZeroPageYAttribute attr;

        public ZeroPageYTest() : base()
        {
            attr = new ZeroPageYAttribute();
        }

        [Theory]
        [InlineData(
            10,
            40,
            50
        )]
        [InlineData(
            250,
            16,
            10
        )]
        public void ReturnsAddress(
            byte inValue,
            byte yValue,
            int expected
        )
        {
            byte[] input = { inValue };
            machine.CPU.Y.Value = yValue;

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(expected, res);
        }
    }
}
