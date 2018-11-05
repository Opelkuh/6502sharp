using Xunit;
using _6502sharp.Instructions;

namespace _6502sharp.Test.Instructions
{
    public class ADC_NMOSTest : NMOSMachine
    {
        ADC _adc;

        public ADC_NMOSTest()
        {
            _adc = new ADC(_cpu);
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

            Assert.Equal(expected, _cpu.A.Value);
        }

        [Fact]
        public void AddsWithMemAddress()
        {
            int memLocation = 0xF88F;

            PrepareCpu(0x32, true);

            _ram.Set(memLocation, 0x36);

            _adc.ADC_Memory(memLocation);

            Assert.Equal(0x69, _cpu.A.Value);
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
            _cpu.DecimalMode = true;
            _cpu.SR.Decimal = true;

            _adc.ADC_Immediate((byte)value);

            Assert.Equal(expected, _cpu.A.Value);
        }

        private void PrepareCpu(int accu, bool carry)
        {
            _cpu.A.Value = (byte)accu;
            _cpu.SR.Carry = carry;
        }
    }
}
