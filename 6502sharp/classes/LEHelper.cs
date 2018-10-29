namespace _6502sharp
{
    /// <summary>
    /// Little-endian coverter
    /// </summary>
    public static class LEHelper {
        /// <summary>
        /// Converts little-endian byte array to int
        /// </summary>
        /// <param name="input">le bytes</param>
        /// <returns>converted number</returns>
        public static int From(params byte[] input) {
            int ret = 0;

            for(int i = 0; i < input.Length; i++) {
                ret |= input[i] << 8 * i;
            }

            return ret;
        }
    }
}
