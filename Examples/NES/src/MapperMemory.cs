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

        private Mirror[] mirrors = new Mirror[] {
            new Mirror(0x0000, 0x07FF, 0x0800, 0x1FFF),
        };
        private PictureProcessingUnit _ppu;
        private IMapper _mapper;

        public MapperMemory(PictureProcessingUnit ppu) : base(65536)
        {
            _ppu = ppu;
        }

        public override byte Get(int location)
        {
            // check ppu
            byte? ppuRes = _ppu.Get(location);
            if (ppuRes.HasValue) return ppuRes.Value;

            // check mapper
            if (_mapper != null)
            {
                byte? res = _mapper.Get(location);

                // return if handled by mapper
                if (res.HasValue) return res.Value;
            }

            // fallback to ram
            CheckMirror(ref location);
            return base.Get(location);
        }

        public override void Set(int location, byte value)
        {
            // return if handled by ppu
            if (_ppu.Set(location, value)) return;

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
