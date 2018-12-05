using Xunit;
using _6502sharp;

namespace _6502sharp.Test.Stacks
{
    public class JSRTest : MachineNMOSBase
    {
        Stack _stack;

        public JSRTest()
        {
            _stack = new Stack(machine.CPU);
        }

        [Theory]
        [InlineData(0xFF, 15, 0xF0)]
        [InlineData(0x51, 1, 0x50)]
        [InlineData(0x00, 16, 0xF0)]
        public void PushesValuesAndDecrementsSP(byte sp, int length, byte expSp)
        {
            byte dummyVal = 0xAB;

            byte[] values = new byte[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = dummyVal;
            }

            machine.CPU.SP.Value = sp;

            _stack.Push(values);

            // assert
            for (int i = 0; i < length; i++)
            {
                int decSp = (sp - i) & 0xFF;
                int expMemAddr = _stack.Offset + decSp;
                byte savedValue = machine.Memory.Get(expMemAddr);

                Assert.Equal(dummyVal, savedValue);
            }

            Assert.Equal(expSp, machine.CPU.SP.Value);
        }

        [Fact]
        public void PushesPC()
        {
            ushort expPc = 0xABBA;
            byte sp = 0xFF;

            machine.CPU.SP.Value = sp;
            machine.CPU.PC.Value = expPc;

            _stack.PushPC();

            // assert
            byte pcHigh = machine.Memory.Get(_stack.Offset + sp);
            byte pcLow = machine.Memory.Get(_stack.Offset + --sp);

            Assert.Equal(expPc >> 8, pcHigh);
            Assert.Equal(expPc & 0xFF, pcLow);
        }

        [Fact]
        public void PopsValue()
        {
            byte val = 0x69;
            byte sp = 0xFF;

            machine.CPU.Memory.Set(_stack.Offset + sp, val);
            machine.CPU.SP.Value = (byte)(sp - 1);

            byte popped = _stack.Pop();

            Assert.Equal(val, popped);
        }

        [Fact]
        public void PopsPC()
        {
            byte[] rawPc = { 0xAB, 0xBA };
            byte sp = 0xFF;

            machine.Memory.Set(_stack.Offset + sp--, rawPc[0]);
            machine.Memory.Set(_stack.Offset + sp--, rawPc[1]);
            machine.CPU.SP.Value = sp;

            int popped = _stack.PopPC();

            // assert
            int expected = (rawPc[0] << 8) | rawPc[1];
            Assert.Equal(expected, popped);
        }
    }
}