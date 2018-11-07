using System;

namespace _6502sharp.Helpers
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

        /// <summary>
        /// Converts int to little-endian byte array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bytes">number of bytes to be converted</param>
        /// <returns>le byte array</returns>
        public static byte[] To(int input, int bytes) {
            byte[] ret = new byte[bytes];

            for(int i = 0; i < bytes; i++) {
                ret[i] = (byte)((input >> 8 * i) & 0xFF);
            }

            return ret;
        }
    }
}
