using Xunit;
using _6502sharp.Reflection;

namespace _6502sharp.Test.Memory
{
    public class AbsoluteTest : MachineNMOSBase
    {
        AbsoluteAttribute attr;

        public AbsoluteTest() : base()
        {
            attr = new AbsoluteAttribute();
        }

        [Fact]
        public void ReturnsAddress()
        {
            byte[] input = { 0xFF, 0xEE };

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0xEEFF, res);
        }
    }
}
