using Xunit;
using _6502sharp.Helpers;

namespace _6502sharp.Test.Helpers
{
    public class LEHelperTest
    {
        [Theory]
        [InlineData(new byte[] { 0xBB, 0xAA }, 0xAABB)]
        [InlineData(new byte[] { 0x80, 0x50, 0x69 }, 0x695080)]
        [InlineData(new byte[] { 0, 0, 0xF0 }, 0xF00000)]
        public void ReturnsCorrectNumber(byte[] input, int expected)
        {
            int res = LEHelper.From(input);

            Assert.Equal(expected, res);
        }

        [Theory]
        [InlineData(0xABCD, 1, new byte[] { 0xCD })]
        [InlineData(0xABBA, 2, new byte[] { 0xBA, 0xAB })]
        [InlineData(0xBADC0D, 3, new byte[] { 0x0D, 0xDC, 0xBA })]
        public void ReturnsCorrectArray(int input, int bytes, byte[] expected) {
            byte[] res = LEHelper.To(input, bytes);

            Assert.Equal(expected, res);
        }
    }
}