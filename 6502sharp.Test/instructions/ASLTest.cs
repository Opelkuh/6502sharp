using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class ASLTest : MachineNMOSBase
    {
        ASL _asl;

        public ASLTest()
        {
            _asl = new ASL(machine.CPU);
        }

        [Theory]
        [InlineData(0x05, 0x0A)]
        [InlineData(0xFF, 0xFE)]
        public void ShiftsAccumulator(
            int accu,
            int expected
        )
        {
            machine.CPU.A.Value = (byte)accu;

            _asl.ASL_Accumulator();

            Assert.Equal(expected, machine.CPU.A.Value);
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

            machine.Memory.Set(memLoc, (byte)value);

            _asl.ASL_Memory(memLoc);

            Assert.Equal(expected, machine.Memory.Get(memLoc));
        }
    }
}
