using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class BRKTest : MachineNMOSBase
    {
        BRK _bit;

        public BRKTest()
        {
            _bit = new BRK(machine.CPU);
        }

        [Fact]
        public void PushesOntoStack()
        {
            int oldPc = 0xBADA;
            byte oldSr = 0xEF;

            byte jumpLow = 0xBA;
            byte jumpHigh = 0xAB;

            machine.CPU.PC.Value = oldPc;
            machine.CPU.SR.Value = oldSr;
            machine.Memory.Set(0xFFFE, jumpLow);
            machine.Memory.Set(0xFFFF, jumpHigh);

            _bit.BRK_Implied();
            
            // check interrupt flag
            AssertFlag.Interrupt(machine, true);

            // check stack
            byte sr = machine.CPU.Stack.Pop();
            byte[] rawPc = machine.CPU.Stack.PopMultiple(2);

            int pc = rawPc[0] | rawPc[1] << 8;

            Assert.Equal(oldPc + 1, pc);
            Assert.Equal(0xFF, sr);

            // check new PC
            Assert.Equal(jumpLow | jumpHigh << 8, machine.CPU.PC.Value);
        }
    }
}
