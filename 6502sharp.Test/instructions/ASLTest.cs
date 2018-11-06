using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class ASLTest : NMOSMachine
    {
        ASL _asl;

        public ASLTest()
        {
            _asl = new ASL(_cpu);
        }

        [Theory]
        [InlineData(0x05, 0x0A)]
        [InlineData(0xFF, 0xFE)]
        public void ShiftsAccumulator(
            int accu,
            int expected
        )
        {
            _cpu.A.Value = (byte)accu;

            _asl.ASL_Accumulator();

            Assert.Equal(expected, _cpu.A.Value);
        }

        [Theory]
        [InlineData(0x05, 0x0A)]
        [InlineData(0xFF, 0xFE)]
        public void ShiftsMemory(
            int value,
            int expected
        )
        {
            int memLoc = 0xBADC;

            _ram.Set(memLoc, (byte)value);

            _asl.ASL_Memory(memLoc);

            Assert.Equal(expected, _ram.Get(memLoc));
        }
    }
}
