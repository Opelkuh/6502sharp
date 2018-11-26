namespace _6502sharp
{
    /// <summary>
    /// Processor register
    /// </summary>
    public class Register : IRegister8Bit
    {
        private byte _store = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <value>Value of the register</value>
        public byte Value
        {
            get
            {
                return _store;
            }

            set
            {
                _store = value;
            }
        }

        public int Size => 1;

        public Register()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue">initial value of register</param>
        public Register(byte startValue)
        {
            _store = startValue;
        }

        public void Set(int location, byte value)
        {
            Value = value;
        }

        public byte Get(int location)
        {
            return Value;
        }
    }
}
