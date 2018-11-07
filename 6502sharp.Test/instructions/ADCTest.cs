using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class ADCTest : MachineNMOSBase
    {
        ADC _adc;

        public ADCTest()
        {
            _adc = new ADC(machine.CPU);
        }

        [Theory]
        [InlineData(0x00, false, 0x50, 0x50)]
        [InlineData(0xAB, false, 0xBA, 0x65)]
        [InlineData(0x08, true, 0x60, 0x69)]
        [InlineData(0xF9, true, 0x07, 0x01)]
        public void AddsWithImmediate(
            int accu,
            bool carry,
            int value,
            int expected
        )
        {
            PrepareCpu(accu, carry);

            _adc.ADC_Immediate((byte)value);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x90, 0x80, true, false, false, true)]
        [InlineData(0xFF, 0x01, true, true, false, false)]
        [InlineData(0x40, 0x40, false, false, true, true)]
        public void AddsAndSetsFlags(
            int accu,
            int value,
            bool expC,
            bool expZ,
            bool expN,
            bool expV
        )
        {
            PrepareCpu(accu, false);

            _adc.ADC_Immediate((byte)value);

            AssertFlag.Carry(machine, expC);
            AssertFlag.Zero(machine, expZ);
            AssertFlag.Negative(machine, expN);
            AssertFlag.Overflow(machine, expV);
        }

        [Fact]
        public void AddsWithMemAddress()
        {
            int memLocation = 0xF88F;

            PrepareCpu(0x32, true);

            machine.Memory.Set(memLocation, 0x36);

            _adc.ADC_Memory(memLocation);

            Assert.Equal(0x69, machine.CPU.A.Value);
        }

        [Theory]
        [InlineData(0x00, false, 0x50, 0x50)]
        [InlineData(0x15, false, 0x07, 0x22)]
        [InlineData(0x29, true, 0x01, 0x31)]
        [InlineData(0x95, true, 0x19, 0x15)]
        public void AddsWithDecimalMode(
            int accu,
            bool carry,
            int value,
            int expected
        )
        {
            PrepareCpu(accu, carry);
            machine.CPU.DecimalMode = true;
            machine.CPU.SR.Decimal = true;

            _adc.ADC_Immediate((byte)value);

            Assert.Equal(expected, machine.CPU.A.Value);
        }

        private void PrepareCpu(int accu, bool carry)
        {
            machine.CPU.A.Value = (byte)accu;
            machine.CPU.SR.Carry = carry;
        }
    }
}
