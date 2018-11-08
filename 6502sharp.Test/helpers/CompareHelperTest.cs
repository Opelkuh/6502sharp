using Xunit;
using _6502sharp.Helpers;

namespace _6502sharp.Test.Helpers
{
    public class CompareHelperTest : MachineNMOSBase
    {
        [Theory]
        [InlineData(0x10, 0x10, true, true, false)]
        [InlineData(0x85, 0x04, true, false, true)]
        [InlineData(0x10, 0xAB, false, false, false)]
        public void SetsCorrectFlags(
            int register,
            int value,
            bool expC,
            bool expZ,
            bool expN
        )
        {
            machine.CPU.SR.Carry = false;
            machine.CPU.SR.Zero = false;
            machine.CPU.SR.Negative = false;

            CompareHelper.RegisterAndValue(machine.CPU, (byte)register, (byte)value);

            AssertFlag.Carry(machine, expC);
            AssertFlag.Zero(machine, expZ);
            AssertFlag.Negative(machine, expN);
        }
    }
}