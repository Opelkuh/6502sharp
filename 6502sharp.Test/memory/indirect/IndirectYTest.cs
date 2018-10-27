using Xunit;

namespace _6502sharp.Test.Memory
{
    public class IndirectYTest : MemoryResolverTestBase
    {
        IndirectYAddressAttribute attr;

        public IndirectYTest() : base()
        {
            attr = new IndirectYAddressAttribute();
        }

        [Theory]
        [InlineData(
            0x10,
            0x10,
            0x11
        )]
        [InlineData(
            0xFF,
            0xFF,
            0x00
        )]
        public void ReturnsAddress(
            byte inValue,
            int memAddr1,
            int memAddr2
        )
        {
            byte[] input = { inValue };
            machine.CPU.Y.Value = 0xFF;
            machine.CPU.Memory.Set(memAddr1, 0xB0);
            machine.CPU.Memory.Set(memAddr2, 0x0A);

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0x0AB0 + 0xFF, res);
        }

        [Fact]
        public void AddsCyclePenalty() {
            byte[] input = { 0x00 };
            machine.CPU.SleepCycles = 0;
            machine.CPU.Y.Value = 1;
            machine.CPU.Memory.Set(0x00, 0xFF);
            machine.CPU.Memory.Set(0x01, 0xEE);

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0xEF00, res);
            Assert.Equal(1, machine.CPU.SleepCycles);
        }
    }
}
