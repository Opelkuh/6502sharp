namespace _6502sharp
{
    public interface ICpu
    {
        /// <summary>
        /// Executes one cpu cycle
        /// </summary>
        void Tick();

        /// <summary>
        /// The number of cycles that the processor is going to sleep for 
        /// (used to time instructions that take multiple cycles)
        /// </summary>
        int SleepCycles { get; set; }

        /// <summary>
        /// Type of the CPU
        /// </summary>
        CPUType Type { get; }

        /// <summary>
        /// Enables or disables decimal mode.
        /// If false, Decimal flag will be ignored
        /// </summary>
        bool DecimalMode { get; }
        
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