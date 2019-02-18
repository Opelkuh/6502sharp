namespace NES
{
    class Range
    {
        public readonly int Start;
        public readonly int End;

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public bool Fits(int address)
        {
            return address >= Start && address <= End;
        }

        public static bool Fits(int start, int end, int address)
        {
            return address >= start && address <= end;
        }
    }
}
