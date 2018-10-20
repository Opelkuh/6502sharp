namespace _6502sharp
{
    public enum StatusFlag
    {
        Carry = 1,
        Zero = 2,
        Interrupt = 4,
        Decimal = 8,
        Break = 16,
        Unused = 32,
        Overflow = 64,
        Negative = 128
    }
}
