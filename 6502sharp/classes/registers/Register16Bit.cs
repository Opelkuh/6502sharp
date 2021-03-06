using _6502sharp.Helpers;

namespace _6502sharp
{

    /// <summary>
    /// 16-bit processor register
    /// </summary>
    public class Register16Bit : IRegister16Bit
    {
        private byte[] _store = new byte[2];

        public int Size => 2;

        /// <summary>
        /// 
        /// </summary>
        /// <value>Value of the register</value>
        public ushort Value
        {
            get
            {
                return (ushort)LEHelper.From(_store);
            }

            set
            {
                int converted = value & 0xFFFF;
                byte lower = (byte)(converted & 0xFF);
                byte higher = (byte)(converted >> 8);

                Set(0, lower);
                Set(1, higher);
            }
        }

        public byte Get(int location)
        {
            return _store[location];
        }

        public void Set(int location, byte value)
        {
            _store[location] = value;
        }
    }
}
