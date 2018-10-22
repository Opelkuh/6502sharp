namespace _6502sharp
{
    public interface ICpu
    {
        /// <summary>
        /// Executes one cpu cycle
        /// </summary>
        void Tick();

        /// <summary>
        /// Memmory linked to the cpu
        /// </summary>
        IReadable Memory { get; }

        /// <summary>
        /// Status register
        /// </summary>
        StatusRegister SR { get; }
        /// <summary>
        /// Accumulator
        /// </summary>
        Register A { get; }

        /// <summary>
        /// X Register
        /// </summary>
        Register X { get; }

        /// <summary>
        /// Y Register
        /// </summary>
        Register Y { get; }

        /// <summary>
        /// Stack pointer
        /// </summary>
        Register SP { get; }

        /// <summary>
        /// Program counter
        /// </summary>
        Register16Bit PC { get; }
    }
}
