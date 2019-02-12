using Xunit;
using _6502sharp.Reflection;

namespace _6502sharp.Test.Memory
{
    public class ZeroPageXTest : MachineNMOSBase
    {
        ZeroPageXAttribute attr;

        public ZeroPageXTest() : base()
        {
            attr = new ZeroPageXAttribute();
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
            byte xValue,
            int expected
        )
        {
            byte[] input = { inValue };
            machine.CPU.X.Value = xValue;

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(expected, res);
        }
    }
}
