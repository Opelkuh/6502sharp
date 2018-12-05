namespace _6502sharp
{
    public interface IStatusRegister : IRegister8Bit
    {
        /// <summary>
        /// Checks if register has specified flag
        /// </summary>
        /// <param name="flag">flag to check</param>
        /// <returns>whether the flag is set</returns>
        bool HasFlag(StatusFlag flag);

        /// <summary>
        /// Sets flag to specified value
        /// </summary>
        /// <param name="flag">target flag</param>
        /// <param name="value">desired value</param>
        void SetFlag(StatusFlag flag, bool value);

        bool Carry { get; set; }
        bool Zero { get; set; }
        bool Interrupt { get; set; }
        bool Decimal { get; set; }
        bool Break { get; set; }
        bool Unused { get; set; }
        bool Overflow { get; set; }
        bool Negative { get; set; }
    }
}