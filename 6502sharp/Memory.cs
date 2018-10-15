namespace _6502sharp
{
    /// <summary>
    /// Main RAM of the emulator
    /// </summary>
    public class Memory : EventReadableBase
    {
        private byte[] _store;
        public override event SetDelegate SetEvent;

        public Memory(int size)
        {
            _store = new byte[size];
        }
        public override byte Get(int location)
        {
            return _store[location];
        }

        public override void Set(int location, byte value)
        {
            bool contFlag = SetEvent.Invoke(ref location, _store[location], ref value);

            if (!contFlag) return;

            _store[location] = value;
        }
    }
}
