using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class SBCTest : MachineNMOSBase
    {
        SBC _sbc;

        public SBCTest()
        {
            _sbc = new SBC(machine.CPU);
        }

        [Theory]
        [InlineData(0xFF, false, 0x01, 0xFE)]
        [InlineData(0x00, false, 0x01, 0xFF)]
        [InlineData(0x50, true, 0x0A, 0x45)]
        [InlineData(0x00, true, 0x00, 0xFF)]
        public void SubstractsImmediate(
            byte accu,
            bool carry,
            byte value,
            byte expected
        )
        {
            PrepareCpu(accu, carry);

            _sbc.SBC_Immediate((byte)value);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0xFF, false, 0x01, 0xFE)]
        [InlineData(0x00, false, 0x01, 0xFF)]
        [InlineData(0x50, true, 0x0A, 0x45)]
        [InlineData(0x00, true, 0x00, 0xFF)]
        public void SubstractsMemory(
            byte accu,
            bool carry,
            byte value,
            byte expected
        )
        {
            int memLocation = 0xF88F;

            PrepareCpu(accu, carry);

            machine.Memory.Set(memLocation, value);

            _sbc.SBC_Memory(memLocation);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x90, 0x80, true, false, false, true)]
        [InlineData(0x00, 0x01, false, false, true, true)]
        [InlineData(0x10, 0x10, true, true, false, false)]
        public void SetsCorrectFlags(
            byte accu,
            byte value,
            bool expC,
            bool expZ,
            bool expN,
            bool expV
        )
        {
            PrepareCpu(accu, false);

            _sbc.SBC_Immediate(value);

            AssertFlag.Carry(machine, expC);
            AssertFlag.Zero(machine, expZ);
            AssertFlag.Negative(machine, expN);
            AssertFlag.Overflow(machine, expV);
        }

        [Theory]
        [InlineData(0x50, false, 0x25, 0x25)]
        [InlineData(0x15, false, 0x07, 0x8)]
        [InlineData(0x99, true, 0x49, 0x49)]
        [InlineData(0x50, true, 0x24, 0x25)]
        public void SubstractsWithDecimalMode(
            byte accu,
            bool carry,
            byte value,
            byte expected
        )
        {
            PrepareCpu(accu, carry);
            machine.CPU.DecimalMode = true;
            machine.CPU.SR.Decimal = true;

            _sbc.SBC_Immediate(value);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        private void PrepareCpu(byte accu, bool carry)
        {
            machine.CPU.A.Value = accu;
            machine.CPU.SR.Carry = carry;
        }
    }
}
