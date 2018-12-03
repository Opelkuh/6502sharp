namespace _6502sharp
{
    public interface ICpu
    {
        /// <summary>
        /// Executes one cpu cycle
        /// </summary>
        void Tick();

        /// <summary>
        /// Interrupts the processor with IRQ
        /// </summary>
        /// <param name="queue">whether to wait for interrupt flag to clear</param>
        void InterruptIRQ(bool queue);

        /// <summary>
        /// Interrupts the processor with NMI
        /// </summary>
        void InterruptNMI();

        /// <summary>
        /// The number of cycles that the processor is going to sleep for 
        /// (used to time instructions that take multiple cycles)
        /// </summary>
        int SleepCycles { get; set; }

        /// <summary>
        /// The number of finished cycles in the CPUs lifetime
        /// </summary>
        int FinishedCycles { get; }

        /// <summary>
        /// Type of the CPU
        /// </summary>
        CPUType Type { get; }

        /// <summary>
        /// Enables or disables decimal mode.
        /// If false, Decimal flag will be ignored
        /// </summary>
        bool DecimalMode { get; set; }
        
        /// <summary>
        /// Memmory linked to the cpu
        /// </summary>
        IReadable Memory { get; }

        /// <summary>
        /// Stack handler of the CPU
        /// </summary>
        IStack Stack { get; set; }

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