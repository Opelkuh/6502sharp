using _6502sharp;

namespace NES
{
    class MapperMemory : Memory
    {
        Mirror[] mirrors = new Mirror[] {
            new Mirror(0x0000, 0x07FF, 0x0800, 0x1FFF),
            new Mirror(0x2000, 0x2007, 0x2008, 0x3FFF)
    };

        public MapperMemory() : base(65536)
        {
        }

        public override byte Get(int location)
        {
            CheckMirror(ref location);
            return base.Get(location);
        }

        public override void Set(int location, byte value)
        {
            CheckMirror(ref location);
            base.Set(location, value);
        }

        private void CheckMirror(ref int loc)
        {
            foreach (var mir in mirrors)
            {
                mir.Transform(ref loc);
            }
        }
    }
}
