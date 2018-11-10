namespace _6502sharp.Helpers
{
    /// <summary>
    /// Class that sets flags based on results
    /// </summary>
    public static class FlagHelper
    {

        /// <summary>
        /// Sets or resets the Carry flag
        /// </summary>
        /// <param name="cpu">target</param>
        /// <param name="value">value to be checked</param>
        public static void SetCarry(ICpu cpu, int value)
        {
            if (value > 255) cpu.SR.Carry = true;
            else cpu.SR.Carry = false;
        }

        /// <summary>
        /// Sets or resets the Overflow flag based on result and values that made the result
        /// </summary>
        /// <param name="cpu">target</param>
        /// <param name="result">result to be checked</param>
        /// <param name="in1">original value 1</param>
        /// <param name="in2">original value 2</param>
        public static void SetOverflow(ICpu cpu, int result, int in1, int in2)
        {
            int calculated = (result ^ in1) & (result ^ in2) & 0x80;
            if (calculated > 0) cpu.SR.Overflow = true;
            else cpu.SR.Overflow = false;
        }

        /// <summary>
        /// Sets or resets the Zero flag
        /// </summary>
        /// <param name="cpu">target</param>
        /// <param name="result">value to be checked</param>
        public static void SetZero(ICpu cpu, int value)
        {
            if ((value & 0xFF) == 0) cpu.SR.Zero = true;
            else cpu.SR.Zero = false;
        }

        /// <summary>
        /// Sets or resets the Negative flag
        /// </summary>
        /// <param name="cpu">target</param>
        /// <param name="value">value to be checked</param>
        public static void SetNegative(ICpu cpu, int value)
        {
            if ((value & 0x80) > 0) cpu.SR.Negative = true;
            else cpu.SR.Negative = false;
        }

        /// <summary>
        /// Sets or resets Zero and Negative flags
        /// </summary>
        /// <param name="cpu">target</param>
        /// <param name="value">value to be checked</param>
        public static void SetNegativeAndZero(ICpu cpu, int value) {
            SetNegative(cpu, value);
            SetZero(cpu, value);
        }
    }
}