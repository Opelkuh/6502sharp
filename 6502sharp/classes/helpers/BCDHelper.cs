namespace _6502sharp.Helpers
{
    /// <summary>
    /// Helper for binary coded decimal adjustments
    /// </summary>
    public static class BCDHelper
    {
        /// <summary>
        /// Gets the (half) carry flag from addition of the lower 4-bits
        /// </summary>
        /// <param name="values">values of the addition</param>
        /// <returns></returns>
        public static bool GetHalfCarry(params int[] values)
        {
            int temp = 0;
            foreach (int i in values)
            {
                temp += i & 0x0F;
            }

            return temp > 0x0F;
        }

        /// <summary>
        /// Gets the (half) borrow flag from substraction of the lower 4-bits
        /// </summary>
        /// <param name="values">values of the substraction</param>
        /// <returns></returns>
        public static bool GetHalfBorrow(params int[] values)
        {
            if (values.Length < 1) return false;

            int temp = values[0] & 0x0F;

            for (int i = 1; i < values.Length; i++)
            {
                temp -= values[i] & 0x0F;
            }

            return temp < 0;
        }

        /// <summary>
        /// Corrects the result of addition of two BCD values (DAA instruction in other processors)
        /// Also adjusts the C flag on all processors and the N and Z flags on CMOS processors
        /// </summary>
        /// <param name="cpu">target cpu</param>
        /// <param name="result">value to be adjusted</param>
        /// <param name="originalValues">values that made the result</param>
        public static void AdditionAdjust(ICpu cpu, ref int result, params int[] originalValues)
        {
            bool halfCarry = GetHalfCarry(originalValues);

            AdditionAdjust(cpu, ref result, halfCarry);
        }

        /// <summary>
        /// Corrects the addition of two BCD values (DAA instruction in other processors)
        /// Also adjusts the C flag on all processors and the N and Z flags on CMOS processors
        /// </summary>
        /// <param name="cpu">target cpu</param>
        /// <param name="result">value to be adjusted</param>
        /// <param name="halfCarry">carry of the lower 4-bits</param>
        public static void AdditionAdjust(ICpu cpu, ref int result, bool halfCarry)
        {
            adjustResult(cpu, ref result, halfCarry, 1);
        }

        /// <summary>
        /// Corrects the result of substraction of two BCD values (DAS instruction in other processors)
        /// Also adjusts the C flag on all processors and the N and Z flags on CMOS processors
        /// </summary>
        /// <param name="cpu">target cpu</param>
        /// <param name="result">value to be adjusted</param>
        /// <param name="originalValues">values that made the result</param>
        public static void SubstractionAdjust(ICpu cpu, ref int result, params int[] originalValues)
        {
            bool halfBorrow = GetHalfBorrow(originalValues);

            SubstractionAdjust(cpu, ref result, halfBorrow);
        }

        /// <summary>
        /// Corrects the result of substraction of two BCD values (DAS instruction in other processors)
        /// Also adjusts the C flag on all processors and the N and Z flags on CMOS processors
        /// </summary>
        /// <param name="cpu">target cpu</param>
        /// <param name="result">value to be adjusted</param>
        /// <param name="halfBorrow">borrow of the lower 4-bits</param>
        public static void SubstractionAdjust(ICpu cpu, ref int result, bool halfBorrow)
        {
            adjustResult(cpu, ref result, halfBorrow, -1);
        }

        private static void adjustResult(ICpu cpu, ref int result, bool carryOrBorrow, sbyte modifier)
        {
            int mod = 0x00;

            // low 4 bits
            if ((result & 0x0F) > 0x09 || carryOrBorrow)
            {
                mod |= 0x06;
            }

            // high 4 bits
            if (result > 0x99)
            {
                mod |= 0x60;
                cpu.SR.Carry = true;
            }
            else cpu.SR.Carry = false;

            result += mod * modifier;

            // recalculate flags on CMOS
            if (cpu.Type == CPUType.CMOS)
            {
                FlagHelper.SetZero(cpu, result);
                FlagHelper.SetNegative(cpu, result);
                cpu.SleepCycles++;
            }
        }
    }
}