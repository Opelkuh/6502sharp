using Xunit;
using System.IO;

namespace _6502sharp.Test.ROMs
{
    public class AllSuiteATest : MachineNMOSBase
    {
        [Fact]
        public void PassesAllSuiteA() {
            LoadTestRom(@"../../../roms/binaries/AllSuiteA.bin", 0x4000);

            machine.CPU.PC.Value = 0x4000;

            while(machine.CPU.PC.Value < 0x45C0 && machine.CPU.FinishedCycles < 0xFFFF) {
                machine.CPU.Tick();
                machine.CPU.SleepCycles = 0;
            }

            byte result = machine.Memory.Get(0x0210);

            Assert.True(result == 0xFF, $"AllSuiteA test rom failed!! Failed test: '{result}'");
        }

        private void LoadTestRom(string path, int offset)
        {
            byte[] data = File.ReadAllBytes(path);

            for (int i = 0; i < data.Length; i++)
            {
                machine.Memory.Set(i + offset, data[i]);
            }
        }
    }
}
