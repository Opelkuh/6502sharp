namespace _6502sharp
{
    public interface IRegister16Bit : IReadable
    {
        /// <summary>
        /// Value of the register
        /// </summary>
        ushort Value { get; set; }
    }
}
