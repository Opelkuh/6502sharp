namespace _6502sharp
{
    public class StatusRegister : Register
    {
        /// <summary>
        /// Checks if register has specified flag
        /// </summary>
        /// <param name="flag">flag to check</param>
        /// <returns>whether the flag is set</returns>
        public bool HasFlag(StatusFlag flag)
        {
            return (Value & (byte)flag) > 0;
        }

        /// <summary>
        /// Sets flag to specified value
        /// </summary>
        /// <param name="flag">target flag</param>
        /// <param name="value">desired value</param>
        public void SetFlag(StatusFlag flag, bool value)
        {
            if (value)
            {
                Value |= (byte)flag;
            }
            else
            {
                Value &= (byte)(~flag);
            }
        }

        public bool Carry
        {
            get
            {
                return HasFlag(StatusFlag.Carry);
            }

            set
            {
                SetFlag(StatusFlag.Carry, value);
            }
        }

        public bool Zero
        {
            get
            {
                return HasFlag(StatusFlag.Zero);
            }

            set
            {
                SetFlag(StatusFlag.Zero, value);
            }
        }

        public bool Interrupt
        {
            get
            {
                return HasFlag(StatusFlag.Interrupt);
            }

            set
            {
                SetFlag(StatusFlag.Interrupt, value);
            }
        }

        public bool Decimal
        {
            get
            {
                return HasFlag(StatusFlag.Decimal);
            }

            set
            {
                SetFlag(StatusFlag.Decimal, value);
            }
        }

        public bool Break
        {
            get
            {
                return HasFlag(StatusFlag.Break);
            }

            set
            {
                SetFlag(StatusFlag.Break, value);
            }
        }

        public bool Unused
        {
            get
            {
                return HasFlag(StatusFlag.Unused);
            }

            set
            {
                SetFlag(StatusFlag.Unused, value);
            }
        }

        public bool Overflow
        {
            get
            {
                return HasFlag(StatusFlag.Overflow);
            }

            set
            {
                SetFlag(StatusFlag.Overflow, value);
            }
        }

        public bool Negative
        {
            get
            {
                return HasFlag(StatusFlag.Negative);
            }

            set
            {
                SetFlag(StatusFlag.Negative, value);
            }
        }
    }
}
