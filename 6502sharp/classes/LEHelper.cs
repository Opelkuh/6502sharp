namespace _6502sharp
{
    public static class LEHelper {
        public static int From(params byte[] input) {
            int ret = 0;

            for(int i = 0; i < input.Length; i++) {
                ret |= input[i] << 8 * i;
            }

            return ret;
        }
    }
}
