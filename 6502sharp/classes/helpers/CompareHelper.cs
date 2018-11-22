using System;

namespace _6502sharp.Helpers
{
    /// <summary>
    /// Helper for compare instructions
    /// </summary>
    public class CompareHelper
    {
        private ICpu _cpu;

        /// <summary>
        /// Creates a new instance of CompareHelper
        /// </summary>
        /// <param name="cpu">cpu to set flags on</param>
        public CompareHelper(ICpu cpu) {
            _cpu = cpu;
        }

        /// <summary>
        /// Compares 2 values and sets flags (C, Z, N) based on the result
        /// </summary>
        /// <param name="registerVal">value of the register</param>
        /// <param name="compareVal">value to be compared with register</param>
        public void RegisterAndValue(byte registerVal, byte compareVal)
        {
            int substracted = registerVal - compareVal;

            FlagHelper.SetNegative(_cpu, substracted);

            // set zero flag (==)
            FlagHelper.SetZero(_cpu, substracted);

            // set carry flag (>=)
            if(registerVal >= compareVal) _cpu.SR.Carry = true;
            else _cpu.SR.Carry = false;
        }
    }
}
