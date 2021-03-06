using Xunit;
using _6502sharp.Reflection;

namespace _6502sharp.Test.Memory
{
    public class ZeroPageTest : MachineNMOSBase
    {
        ZeroPageAttribute attr;

        public ZeroPageTest() : base()
        {
            attr = new ZeroPageAttribute();
        }

        [Fact]
        public void ReturnsAddress()
        {
            byte[] input = { 0xAB };

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0xAB, res);
        }
    }
}
