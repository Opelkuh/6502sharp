using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class DECTest : MachineCMOSBase
    {
        DEC _dec;

        public DECTest()
        {
            _dec = new DEC(machine.CPU);
        }

        [Theory]
        [InlineData(0x50, 0x4F)]
        [InlineData(0x11, 0x10)]
        [InlineData(0x00, 0xFF)]
        public void DecrementsAccumulator(byte accu, byte expected)
        {
            machine.CPU.A.Value = accu;
            
            _dec.DEC_Accumulator();

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0xF0, 0xEF)]
        [InlineData(0x11, 0x10)]
        [InlineData(0x00, 0xFF)]
        public void DecrementsMemory(byte value, byte expected)
        {
            int memAddress = 0xABBA;

            machine.Memory.Set(memAddress, value);
            
            _dec.DEC_Memory(memAddress);

            Assert.Equal(expected, machine.Memory.Get(memAddress));
        }
    }
}
