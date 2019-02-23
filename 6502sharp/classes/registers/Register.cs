namespace _6502sharp
{
    /// <summary>
    /// Processor register
    /// </summary>
    public class Register : IRegister8Bit
    {
        protected byte store = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <value>Value of the register</value>
        public virtual byte Value
        {
            get
            {
                return store;
            }

            set
            {
                store = value;
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
            store = startValue;
        }

        public virtual void Set(int location, byte value)
        {
            Value = value;
        }

        public virtual byte Get(int location)
        {
            return Value;
        }
    }
}
