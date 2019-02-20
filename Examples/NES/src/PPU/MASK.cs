namespace NES.PPU
{
    class MASK : PPURegisterBase
    {
        public bool Greyscale
        {
            get => HasFlag(1 << 0);
            set => SetFlag(1 << 0, value);
        }

        public bool ShowBackgroundLeftmost
        {
            get => HasFlag(1 << 1);
            set => SetFlag(1 << 1, value);
        }

        public bool ShowSpritesLeftmost
        {
            get => HasFlag(1 << 2);
            set => SetFlag(1 << 2, value);
        }

        public bool ShowBackground
        {
            get => HasFlag(1 << 3);
            set => SetFlag(1 << 3, value);
        }

        public bool ShowSprites
        {
            get => HasFlag(1 << 4);
            set => SetFlag(1 << 4, value);
        }

        public bool ExtraRed
        {
            get => HasFlag(1 << 5);
            set => SetFlag(1 << 5, value);
        }

        public bool ExtraGreen
        {
            get => HasFlag(1 << 6);
            set => SetFlag(1 << 6, value);
        }

        public bool ExtraBlue
        {
            get => HasFlag(1 << 7);
            set => SetFlag(1 << 7, value);
        }
    }
}