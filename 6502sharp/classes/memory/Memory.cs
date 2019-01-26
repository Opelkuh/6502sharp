namespace _6502sharp
{
    /// <summary>
    /// Main RAM of the emulator
    /// </summary>
    public class Memory : IReadable
    {
        private byte[] _store;

        public int Size => _store.Length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">Size of the memory. Max - 65536</param>
        public Memory(int size)
        {
            if(size > 65536) throw new System.Exception("Memory size has to be bellow 65536");

            _store = new byte[size];
        }
        public byte Get(int location)
        {
            return _store[location % Size];
        }

        public void Set(int location, byte value)
        {
            _store[location % Size] = value;
        }
    }
}
