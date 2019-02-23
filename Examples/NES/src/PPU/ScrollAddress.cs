namespace NES.PPU
{
    /*
        Scroll Address
        ---------------------
        yyy NN YYYYY XXXXX
        ||| || ||||| +++++-- coarse X scroll
        ||| || +++++-------- coarse Y scroll
        ||| ++-------------- nametable select
        +++----------------- fine Y scroll
         */

    class ScrollAddress
    {
        public int Value = 0;

        public int FineYScroll
        {
            get => Value >> 12;
            set
            {
                Value =
                    Value &= ~0x7000;
                Value |= (value & 0x07) << 12;
            }
        }
        public int CoarseXScroll
        {
            get => Value & 0x1F;
            set
            {
                Value &= ~0x1F;
                Value |= value & 0x1F;
            }
        }
        public int CoarseYScroll
        {
            get => (Value >> 5) & 0x1F;
            set
            {
                Value &= ~(0x1F << 5);
                Value |= (value & 0x1F) << 5;
            }
        }
        public int NametableSelect
        {
            get => (Value >> 10) & 0x03;
            set
            {
                Value &= ~(0x03) << 10;
                Value |= (value & 0x03) << 10;
            }
        }

        public int TileAddress
        {
            get => 0x2000 | (Value & 0x0FFF);
        }

        public int AttributeAddress
        {
            get => 0x23C0 | (Value & 0x0C00) | ((Value >> 4) & 0x38) | ((Value >> 2) & 0x07);
        }
    }
}