namespace _6502sharp.Helpers
{
    /// <summary>
    /// Class that sets flags based on results
    /// </summary>
    public class FlagHelper
    {
        private ICpu _cpu;

        /// <summary>
        /// Creates a new instance of FlagHelper
        /// </summary>
        /// <param name="cpu">cpu to set flags on</param>
        public FlagHelper(ICpu cpu) {
            _cpu = cpu;
        }

        /// <summary>
        /// Sets or resets the Carry flag
        /// </summary>
        /// <param name="value">value to be checked</param>
        public FlagHelper SetCarry(int value)
        {
            if (value > 255) _cpu.SR.Carry = true;
            else _cpu.SR.Carry = false;

            return this;
        }

        /// <summary>
        /// Sets or resets the Overflow flag based on result and values that made the result
        /// </summary>
        /// <param name="result">result to be checked</param>
        /// <param name="in1">original value 1</param>
        /// <param name="in2">original value 2</param>
        public FlagHelper SetOverflow(int result, int in1, int in2)
        {
            int calculated = (result ^ in1) & (result ^ in2) & 0x80;
            if (calculated > 0) _cpu.SR.Overflow = true;
            else _cpu.SR.Overflow = false;

            return this;
        }

        /// <summary>
        /// Sets or resets the Zero flag
        /// </summary>
        /// <param name="result">value to be checked</param>
        public FlagHelper SetZero(int value)
        {
            if ((value & 0xFF) == 0) _cpu.SR.Zero = true;
            else _cpu.SR.Zero = false;

            return this;
        }

        /// <summary>
        /// Sets or resets the Negative flag
        /// </summary>
        /// <param name="value">value to be checked</param>
        public FlagHelper SetNegative(int value)
        {
            if ((value & 0x80) > 0) _cpu.SR.Negative = true;
            else _cpu.SR.Negative = false;

            return this;
        }

        /// <summary>
        /// Sets or resets Zero and Negative flags
        /// </summary>
        /// <param name="value">value to be checked</param>
        public FlagHelper SetNegativeAndZero(int value) {
            SetNegative(value);
            SetZero(value);

            return this;
        }
    }
}