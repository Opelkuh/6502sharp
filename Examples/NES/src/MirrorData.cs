namespace NES
{
    class Mirror
    {
        public readonly int OriginalStart;
        public readonly int OriginalEnd;
        public readonly int MirrorStart;
        public readonly int MirrorEnd;

        private readonly int wrapDiff;

        public Mirror(int start, int end, int mirStart, int mirEnd)
        {
            OriginalStart = start;
            OriginalEnd = end;
            MirrorStart = mirStart;
            MirrorEnd = mirEnd;

            wrapDiff = OriginalEnd - OriginalStart + 1;
        }

        public bool Fits(int address)
        {
            return address >= MirrorStart && address <= MirrorEnd;
        }

        public int Wrap(int address)
        {
            return ((address - MirrorStart) % wrapDiff) + OriginalStart;
        }
    }
}
