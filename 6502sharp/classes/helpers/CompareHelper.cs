using System;

namespace _6502sharp.Helpers
{
    /// <summary>
    /// Helper for compare instructions
    /// </summary>
    public static class CompareHelper
    {
        /// <summary>
        /// Compares 2 values and sets flags (C, N, Z) based on the result
        /// </summary>
        /// <param name="cpu">cpu reference</param>
        /// <param name="registerVal">value of the register</param>
        /// <param name="compareVal">value to be compared with register</param>
        public static void RegisterAndValue(ICpu cpu, byte registerVal, byte compareVal)
        {
            int substracted = registerVal - compareVal;

            FlagHelper.SetNegative(cpu, substracted);

            // set zero flag (==)
            FlagHelper.SetZero(cpu, substracted);

            // set carry flag (>=)
            if(registerVal >= compareVal) cpu.SR.Carry = true;
            else cpu.SR.Carry = false;
        }
    }
}
