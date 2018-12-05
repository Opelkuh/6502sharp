using Xunit;
using _6502sharp.Helpers;

namespace _6502sharp.Test.Helpers
{
    public class CompareHelperTest : MachineNMOSBase
    {
        private CompareHelper compare;

        public CompareHelperTest() {
            compare = new CompareHelper(machine.CPU);
        }

        [Theory]
        [InlineData(0x10, 0x10, true, true, false)]
        [InlineData(0x85, 0x04, true, false, true)]
        [InlineData(0x10, 0xAB, false, false, false)]
        public void SetsCorrectFlags(
            byte register,
            byte value,
            bool expC,
            bool expZ,
            bool expN
        )
        {
            machine.CPU.SR.Carry = false;
            machine.CPU.SR.Zero = false;
            machine.CPU.SR.Negative = false;

            compare.RegisterAndValue(register, value);

            AssertFlag.Carry(machine, expC);
            AssertFlag.Zero(machine, expZ);
            AssertFlag.Negative(machine, expN);
        }
    }
}