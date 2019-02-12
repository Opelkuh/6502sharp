using Xunit;
using System.IO;

namespace _6502sharp.Test.ROMs
{
    public class AllSuiteATest : MachineNMOSBase
    {
        [Fact]
        public void PassesAllSuiteA()
        {
            machine.LoadRom(@"../../../roms/binaries/AllSuiteA.bin", 0x4000);
            machine.CPU.Reset();

            while (machine.CPU.PC.Value < 0x45C0 && machine.CPU.FinishedCycles < 0xFFFF)
            {
                machine.CPU.NextInstruction();
            }

            byte result = machine.Memory.Get(0x0210);

            Assert.True(result == 0xFF, $"AllSuiteA test rom failed!! Failed test #{result}");
        }
    }
}
