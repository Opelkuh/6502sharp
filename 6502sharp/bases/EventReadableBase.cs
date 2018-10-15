namespace _6502sharp
{
    public abstract class EventReadableBase : IReadable
    {
        public abstract byte Get(int location);
        public abstract void Set(int location, byte value);

        public delegate bool SetDelegate(ref int location, in byte oldValue, ref byte newValue);
        public abstract event SetDelegate SetEvent;
    }
}
