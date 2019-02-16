namespace NES
{

    class Mirror
    {
        public readonly Range Original;
        public readonly Range Target;

        private readonly int wrapDiff;

        public Mirror(int start, int end, int mirStart, int mirEnd) : this(
            new Range(start, end),
            new Range(mirStart, mirEnd)
        )
        {
        }

        public Mirror(Range original, Range target)
        {
            Original = original;
            Target = target;

            wrapDiff = original.End - original.Start + 1;
        }

        public void Transform(ref int address)
        {
            if (!Fits(address)) return;

            address = ((address - Target.Start) % wrapDiff) + Original.Start;
        }

        public bool Fits(int address)
        {
            return Target.Fits(address);
        }
    }
}
