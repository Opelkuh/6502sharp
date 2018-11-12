using Xunit;

namespace _6502sharp.Test.Memory
{
    public class IndirectXTest : MachineCMOSBase
    {
        IndirectXAttribute attr;

        public IndirectXTest() : base()
        {
            attr = new IndirectXAttribute();
        }

        [Theory]
        [InlineData(
            0x20,
            0x04,
            0x24,
            0x25
        )]
        [InlineData(
            0xFE,
            0x01,
            0xFF,
            0x00
        )]
        [InlineData(
            0xFF,
            0x01,
            0x00,
            0x01
        )]
        public void ReturnsAddress(
            byte inValue,
            byte xValue,
            int memAddr1,
            int memAddr2
        )
        {
            byte[] input = { inValue };
            machine.CPU.X.Value = xValue;
            machine.CPU.Memory.Set(memAddr1, 0xB0);
            machine.CPU.Memory.Set(memAddr2, 0x0A);

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0x0AB0, res);
        }
    }
}
