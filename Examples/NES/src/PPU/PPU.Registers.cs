namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        #region Addresses
        private static readonly Mirror REGISTER_MIRROR =
            new Mirror(
                0x2000, 0x2007,
                0x2008, 0x3FFF
            );
        private const int CTRLADDR = 0x2000;
        private const int MASKADDR = 0x2001;
        private const int STATUSADDR = 0x2002;
        private const int OAMADDR = 0x2003;
        private const int OAMDATA = 0x2004;
        private const int SCROLL = 0x2005;
        private const int ADDR = 0x2006;
        private const int DATA = 0x2007;
        private const int OAMDMA = 0x4014;
        #endregion

        #region Registers
        public CTRL CTRL => ctrl;
        public MASK MASK => mask;
        public STATUS STATUS => status;

        private CTRL ctrl = new CTRL();
        private MASK mask = new MASK();
        private STATUS status = new STATUS();
        #endregion

        public byte Latch = 0;

        public byte? Get(int address)
        {
            REGISTER_MIRROR.Transform(ref address);

            switch (address)
            {
                case CTRLADDR:
                case MASKADDR:
                case OAMADDR:
                case SCROLL:
                case ADDR:
                case OAMDMA:
                    return Latch;
                case STATUSADDR:
                    int ret = (status.Value | (Latch & 0x1F));

                    status.VBlank = false;
                    AddressLatch = false;

                    return (byte)ret;
                case OAMDATA:
                    return oam.Get(OAMAddress);
                case DATA:
                    byte buf;
                    if (VRAMAddress <= 0x3EFFF)
                    {
                        buf = ReadBuffer;

                        ReadBuffer = vram.Get(VRAMAddress);
                    }
                    else
                        buf = vram.Get(VRAMAddress);

                    VRAMAddress += ctrl.AddressIncrement;

                    return buf;
                default:
                    return null;
            }
        }

        public bool Set(int address, byte value)
        {
            REGISTER_MIRROR.Transform(ref address);

            switch (address)
            {
                case CTRLADDR:
                    ctrl.Value = value; break;
                case MASKADDR:
                    mask.Value = value; break;
                case OAMADDR:
                    OAMAddress = value; break;
                case OAMDATA:
                    oam.Set(OAMAddress++, value);
                    break;
                case SCROLL:
                    writeScroll(value); break;
                case ADDR:
                    writeAddr(value);
                    break;
                case DATA:
                    vram.Set(VRAMAddress, value);
                    VRAMAddress += ctrl.AddressIncrement;
                    break;
                case OAMDMA:
                    startDMA(value); break;
                case STATUSADDR:
                    break;

                default:
                    return false;
            }

            Latch = value;

            return true;
        }
    }
}
