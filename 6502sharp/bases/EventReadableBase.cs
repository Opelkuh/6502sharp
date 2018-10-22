namespace _6502sharp
{
    /// <summary>
    /// IReadable with added events for manipulating the Set function before it's called
    /// </summary>
    public abstract class EventReadableBase : IReadable
    {
        public abstract int Size { get; }

        public abstract byte Get(int location);
        public abstract void Set(int location, byte value);

        /// <summary>
        /// Function called before the Set function is executed
        /// </summary>
        /// <param name="location">target</param>
        /// <param name="oldValue">old value of the target</param>
        /// <param name="newValue">new value that will be set</param>
        /// <returns>true - continue, false - abort set</returns>
        public delegate bool SetDelegate(ref int location, in byte oldValue, ref byte newValue);

        /// <summary>
        /// Event called before the Set function is
        /// </summary>
        public abstract event SetDelegate SetEvent;
    }
}
