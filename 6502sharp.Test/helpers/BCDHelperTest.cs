using Xunit;
using _6502sharp.Helpers;

namespace _6502sharp.Test.Helpers
{
    public class BCDHelperTest : MachineNMOSBase
    {
        private BCDHelper bcd;

        public BCDHelperTest() {
            bcd = new BCDHelper(machine.CPU);
        }

        [Theory]
        [InlineData(false, 0x01, 0x10)]
        [InlineData(false, 0xF0, 0xF0)]
        [InlineData(true, 0x08, 0x08)]
        [InlineData(true, 0xFF, 0xFF)]
        public void RetursHalfCarryFlag(bool expected, params int[] vals)
        {
            bool ret = BCDHelper.GetHalfCarry(vals);

            Assert.Equal(expected, ret);
        }

        [Theory]
        [InlineData(false, 0x0F, 0x01)]
        [InlineData(false, 0xF0, 0xF0)]
        [InlineData(true, 0x25, 0x57)]
        [InlineData(true, 0xFE, 0xFF)]
        public void RetursHalfBorrowFlag(bool expected, params int[] values)
        {
            bool ret = BCDHelper.GetHalfBorrow(values);

            Assert.Equal(expected, ret);
        }

        [Theory]
        [InlineData(false, 0x36 , 0x30, 0x18, 0x18)]
        [InlineData(true,  0x104, 0x9E, 0x58, 0x46)]
        [InlineData(true,  0x100, 0x9A, 0x50, 0x50)]
        public void AdjustsResultOfAddition(
            bool carryFlag,
            int adjustedResult,
            int result,
            params int[] originalValues
        )
        {
            bcd.AdditionAdjust(ref result, originalValues);

            Assert.Equal(adjustedResult, result);
            Assert.Equal(carryFlag, machine.CPU.SR.Carry);
        }

        [Theory]
        [InlineData(0x28, 0x2E, 0x55, 0x27)]
        [InlineData(0x68, 0xCE, 0x25, 0x57)]
        public void AdjustsResultOfSubstraction(
            int adjustedResult,
            int result,
            params int[] originalValues
        )
        {
            bcd.SubstractionAdjust(ref result, originalValues);

            Assert.Equal(adjustedResult, result);
        }
    }
}