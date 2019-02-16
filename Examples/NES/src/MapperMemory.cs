using _6502sharp;

namespace NES
{
    class MapperMemory : Memory
    {
        public IMapper Mapper
        {
            get => _mapper;
            set
            {
                if (_mapper != null) _mapper.Dispose();

                _mapper = value;
            }
        }

        private Mirror[] mirrors = new Mirror[] {
            new Mirror(0x0000, 0x07FF, 0x0800, 0x1FFF),
            new Mirror(0x2000, 0x2007, 0x2008, 0x3FFF)
        };
        private IMapper _mapper;

        public MapperMemory() : base(65536)
        {
        }

        public override byte Get(int location)
        {
            if (_mapper != null)
            {
                byte? res = _mapper.Get(location);

                // return if handled by mapper
                if (res.HasValue) return res.Value;
            }

            CheckMirror(ref location);
            return base.Get(location);
        }

        public override void Set(int location, byte value)
        {
            // return if handled by mapper
            if (_mapper != null && _mapper.Set(location, value)) return;

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
