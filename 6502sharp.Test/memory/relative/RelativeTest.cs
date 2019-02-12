using Xunit;
using _6502sharp.Reflection;

namespace _6502sharp.Test.Memory
{
    public class RelativeTest : MachineNMOSBase
    {
        RelativeAttribute attr;

        public RelativeTest() : base()
        {
            attr = new RelativeAttribute();
        }

        [Theory]
        [InlineData(0x0010, 0x40, 0x0050)]
        [InlineData(100, -50, 50)]
        public void ReturnsAddress(ushort pc, int offset, int expected)
        {
            byte[] input = { (byte)offset };
            machine.CPU.PC.Value = pc;

            int res = attr.Resolve(machine.CPU, ref input);

            Assert.Equal(expected, res);
        }
    }
}
