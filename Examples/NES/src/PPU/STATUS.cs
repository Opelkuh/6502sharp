namespace NES.PPU
{
    class STATUS : PPURegisterBase
    {
        public bool SpriteOverflow
        {
            get => HasFlag(1 << 5);
            set => SetFlag(1 << 5, value);
        }

        public bool Sprite0Hit
        {
            get => HasFlag(1 << 6);
            set => SetFlag(1 << 6, value);
        }

        public bool VBlank
        {
            get => HasFlag(1 << 7);
            set => SetFlag(1 << 7, value);
        }
    }
}