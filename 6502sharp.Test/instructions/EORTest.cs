using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class EORTest : MachineCMOSBase
    {
        EOR _dec;

        public EORTest()
        {
            _dec = new EOR(machine.CPU);
        }

        [Theory]
        [InlineData(0x81, 0x7E, 0xFF)]
        [InlineData(0xFF, 0xFF, 0x00)]
        [InlineData(0x60, 0xFF, 0x9F)]
        public void XORsImmediate(
            byte accu,
            byte value,
            byte expected
        )
        {
            machine.CPU.A.Value = accu;

            _dec.EOR_Immediate(value);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x81, 0x7E, 0xFF)]
        [InlineData(0xFF, 0xFF, 0x00)]
        [InlineData(0x60, 0xFF, 0x9F)]
        public void XORsMemory(
            byte accu,
            byte value,
            byte expected
        )
        {
            int memAddress = 0xABBA;

            machine.CPU.A.Value = accu;
            machine.Memory.Set(memAddress, value);

            _dec.EOR_Memory(memAddress);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x81, 0x7E, true, false)]
        [InlineData(0xFF, 0xFF, false, true)]
        public void SetsCorrectFlags(
            byte accu,
            byte value,
            bool expN,
            bool expZ
        )
        {
            machine.CPU.A.Value = accu;
            machine.CPU.SR.Negative = false;
            machine.CPU.SR.Zero = false;

            _dec.EOR_Immediate(value);

            AssertFlag.Negative(machine, expN);
            AssertFlag.Zero(machine, expZ);
        }
    }
}
