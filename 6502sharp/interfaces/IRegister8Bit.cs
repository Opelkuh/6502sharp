namespace _6502sharp
{
    /// <summary>
    /// Interface for 8-bit CPU registers
    /// </summary>
    public interface IRegister8Bit : IReadable
    {
        /// <summary>
        /// Value of the register
        /// </summary>
        byte Value { get; set; }
    }
}
