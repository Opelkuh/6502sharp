using Xunit;

namespace _6502sharp.Test.Memory
{
    public class ZeroPageTest : MemoryResolverTestBase
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
