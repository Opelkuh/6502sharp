namespace _6502sharp
{
    public delegate void CpuEventHandler(ICpu cpu);
    public delegate void CpuEventHandler<TEventArgs>(ICpu cpu, TEventArgs e);

    public interface ICpu
    {
        /// <summary>
        /// Executes one cpu cycle
        /// </summary>
        void Tick();

        /// <summary>
        /// Raised after every CPU cycle even if instruction execution was skipped
        /// </summary>
        event CpuEventHandler Cycle;

        /// <summary>
        /// Raised afer an instruction was executed
        /// </summary>
        event CpuEventHandler<InstructionEventArgs> Instruction;

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
        IStatusRegister SR { get; set; }
        /// <summary>
        /// Accumulator
        /// </summary>
        IRegister8Bit A { get; set; }

        /// <summary>
        /// X Register
        /// </summary>
        IRegister8Bit X { get; set; }

        /// <summary>
        /// Y Register
        /// </summary>
        IRegister8Bit Y { get; set; }

        /// <summary>
        /// Stack pointer
        /// </summary>
        IRegister8Bit SP { get; set; }

        /// <summary>
        /// Program counter
        /// </summary>
        IRegister16Bit PC { get; set; }
    }
}