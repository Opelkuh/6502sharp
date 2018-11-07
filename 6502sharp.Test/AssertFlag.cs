using Xunit;

namespace _6502sharp.Test
{
    public static class AssertFlag
    {
        public static void Carry(IMachine machine, bool exp)
        {
            Assert.True(exp == machine.CPU.SR.Carry, "Invalid carry flag");
        }

        public static void Zero(IMachine machine, bool exp)
        {
            Assert.True(exp == machine.CPU.SR.Zero, "Invalid zero flag");
        }

        public static void Negative(IMachine machine, bool exp)
        {
            Assert.True(exp == machine.CPU.SR.Negative, "Invalid negative flag");
        }

        public static void Overflow(IMachine machine, bool exp)
        {
            Assert.True(exp == machine.CPU.SR.Overflow, "Invalid overflow flag");
        }

        public static void Decimal(IMachine machine, bool exp)
        {
            Assert.True(exp == machine.CPU.SR.Decimal, "Invalid decimal flag");
        }

        public static void Interrupt(IMachine machine, bool exp)
        {
            Assert.True(exp == machine.CPU.SR.Interrupt, "Invalid interrupt flag");
        }

        public static void Unused(IMachine machine, bool exp)
        {
            Assert.True(exp == machine.CPU.SR.Unused, "Invalid unused/B flag");
        }
    }
}
