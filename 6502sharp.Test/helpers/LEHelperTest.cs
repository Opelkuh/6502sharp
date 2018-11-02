using Xunit;
using _6502sharp.Helpers;

namespace _6502sharp.Test
{
    public class LEHelperTest
    {
        [Theory]
        [InlineData(0xAABB, new byte[] { 0xBB, 0xAA })]
        [InlineData(0x695080, new byte[] { 0x80, 0x50, 0x69 })]
        [InlineData(0xF00000, new byte[] { 0, 0, 0xF0 })]
        public void ReturnsCorrectNumber(int expected, byte[] input)
        {
            int res = LEHelper.From(input);

            Assert.Equal(expected, res);
        }
    }
}