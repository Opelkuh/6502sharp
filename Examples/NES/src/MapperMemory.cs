using _6502sharp;
using NES.PPU;

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
        public Controller Controller1 = new Controller();
        public Controller Controller2 = new Controller();

        private Mirror[] mirrors = new Mirror[] {
            new Mirror(0x0000, 0x07FF, 0x0800, 0x1FFF),
            new Mirror(
                0x2000, 0x2007,
                0x2008, 0x3FFF
            )
        };

        private GoPPU _ppu;
        private IMapper _mapper;

        public MapperMemory(GoPPU ppu) : base(65536)
        {
            _ppu = ppu;
        }

        public override byte Get(int location)
        {
            // check mapper
            if (_mapper != null)
            {
                byte? res = _mapper.Get(location);

                // return if handled by mapper
                if (res.HasValue) return res.Value;
            }

            // fallback to ram
            CheckMirror(ref location);

            // controllers
            if (location == 0x4016) return Controller1.Get();
            if (location == 0x4018) return Controller2.Get();

            // ppu
            if (Range.Fits(0x2000, 0x2007, location))
            {
                return _ppu.readRegister(location);
            }

            return base.Get(location);
        }

        public override void Set(int location, byte value)
        {
            // return if handled by mapper
            if (_mapper != null && _mapper.Set(location, value)) return;

            CheckMirror(ref location);

            // controllers
            if (location == 0x4016)
            {
                Controller1.Set(value);
                Controller2.Set(value);
            }

            // ppu
            if (Range.Fits(0x2000, 0x2007, location))
            {
                _ppu.writeRegister(location, value);
            }

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
