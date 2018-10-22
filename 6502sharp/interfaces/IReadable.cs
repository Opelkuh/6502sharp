namespace _6502sharp
{
    /// <summary>
    /// Interface for all memory classes
    /// </summary>
    public interface IReadable
    {
        int Size { get; }

        /// <summary>
        /// Gets byte at memory location
        /// </summary>
        /// <param name="location">target</param>
        /// <returns></returns>
        byte Get(int location);

        /// <summary>
        /// Sets memory location to byte
        /// </summary>
        /// <param name="location">target</param>
        /// <param name="value">new value</param>
        void Set(int location, byte value);
    }
}
