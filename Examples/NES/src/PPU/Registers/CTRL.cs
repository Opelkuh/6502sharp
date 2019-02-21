using _6502sharp;

namespace NES.PPU
{
    class CTRL : PPURegisterBase
    {
        public int NametableAddress
        {
            get
            {
                switch (NametableAddressRaw)
                {
                    case 0: return 0x2000;
                    case 1: return 0x2400;
                    case 2: return 0x2800;
                    case 3: return 0x2C00;
                    default: return 0x2000;
                }
            }
        }

        public int NametableAddressRaw
        {
            get => store & 3;
            set
            {
                store = (byte)(store & ~3 | (value & 3));
            }
        }

        public bool AddressIncrementRaw
        {
            get => HasFlag(1 << 2);
            set => SetFlag(1 << 2, value);
        }

        public int AddressIncrement
        {
            get => AddressIncrementRaw ? 32 : 1;
        }

        public bool SpriteAddressRaw
        {
            get => HasFlag(1 << 3);
            set => SetFlag(1 << 3, value);
        }

        public int SpriteAddress
        {
            get
            {
                if (SpriteAddressRaw) return 0x0000;
                else return 0x1000;
            }
        }

        public bool BackgroundAddressRaw
        {
            get => HasFlag(1 << 4);
            set => SetFlag(1 << 4, value);
        }

        public int BackgroundAddress
        {
            get
            {
                if (BackgroundAddressRaw) return 0x0000;
                else return 0x1000;
            }
        }

        public bool SpriteSizeRaw
        {
            get => HasFlag(1 << 5);
            set => SetFlag(1 << 5, value);
        }

        public int SpriteSize
        {
            get => SpriteSizeRaw ? 16 : 8;
        }

        public bool IsMaster
        {
            get => HasFlag(1 << 6);
            set => SetFlag(1 << 6, value);
        }

        public bool NMIEnabled
        {
            get => HasFlag(1 << 7);
            set => SetFlag(1 << 7, value);
        }
    }
}