using Xunit;

namespace _6502sharp.Test.Memory
{
    public class AbsoluteTest : MemoryResolverTestBase
    {
        AbsoluteAddressAttribute attr;

        public AbsoluteTest() : base()
        {
            attr = new AbsoluteAddressAttribute();
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
