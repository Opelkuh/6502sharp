using Xunit;

namespace _6502sharp.Test.Memory
{
    public class IndirectCMOSTest : MachineCMOSBase
    {
        IndirectAttribute attr;

        public IndirectCMOSTest() : base()
        {
            attr = new IndirectAttribute();
        }

        [Theory]
        [InlineData(
            0xB0,
            0x0A,
            0x0AB0,
            0x0AB1
        )]
        [InlineData(
            0xFF,
            0xC0,
            0xC0FF,
            0xC100
        )]
        public void ReturnsCorrectAddress(
            byte in1,
            byte in2,
            int memLoc1,
            int memLoc2
        )
        {
            byte[] input = { in1, in2 };
            machine.CPU.Memory.Set(memLoc1, 0x0F);
            machine.CPU.Memory.Set(memLoc2, 0x0E);

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0x0E0F, res);
        }
    }
}
