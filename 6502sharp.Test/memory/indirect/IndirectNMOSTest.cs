using Xunit;

namespace _6502sharp.Test.Memory
{
    public class IndirectNMOSTest : MemoryResolverTestBase
    {
        IndirectAddressAttribute attr;

        public IndirectNMOSTest() : base()
        {
            attr = new IndirectAddressAttribute();
        }

        [Fact]
        public void ReturnsCorrectAddress()
        {
            byte[] input = { 0xB0, 0x0A };
            machine.CPU.Memory.Set(0x0AB0, 0x0F);
            machine.CPU.Memory.Set(0x0AB1, 0x0E);

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0x0E0F, res);
        }

        [Fact]
        public void ReturnsBuggedAddress()
        {
            byte[] input = { 0xFF, 0xC0 };
            machine.CPU.Memory.Set(0xC0FF, 0x0F);
            machine.CPU.Memory.Set(0xC000, 0x0E);

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(0x0E0F, res);
        }
    }
}
