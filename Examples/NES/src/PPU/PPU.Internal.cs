namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        #region Internal registers
        // OAM
        private byte OAMAddress = 0;
        private byte OAMData = 0;

        // Scrolling
        private int VRAMAddress = 0;
        private int TempVRAMAddress = 0;
        private bool AddressLatch = false;

        /*
        TempVRAMAddress
        ---------------------
        yyy NN YYYYY XXXXX
        ||| || ||||| +++++-- coarse X scroll
        ||| || +++++-------- coarse Y scroll
        ||| ++-------------- nametable select
        +++----------------- fine Y scroll
         */

        private int FineXScroll = 0;
        private int FineYScroll
        {
            get => TempVRAMAddress >> 12;
            set
            {
                TempVRAMAddress =
                    TempVRAMAddress &= ~0x7000;
                TempVRAMAddress |= (value & 0x07) << 12;
            }
        }
        private int CoarseXScroll
        {
            get => TempVRAMAddress & 0x1F;
            set
            {
                TempVRAMAddress &= ~0x1F;
                TempVRAMAddress |= value & 0x1F;
            }
        }
        private int CoarseYScroll
        {
            get => (TempVRAMAddress >> 5) & 0x1F;
            set
            {
                TempVRAMAddress &= ~(0x1F << 5);
                TempVRAMAddress |= (value & 0x1F) << 5;
            }
        }
        private int NametableSelect
        {
            get => (TempVRAMAddress >> 10) & 0x03;
            set
            {
                TempVRAMAddress &= ~(0x03) << 10;
                TempVRAMAddress |= (value & 0x03) << 10;
            }
        }

        private byte ReadBuffer = 0;
        #endregion

        private void writeScroll(byte value)
        {
            int newFine = value >> 3;
            int newCoarse = value & 0x07;

            if (!AddressLatch)
            {
                FineXScroll = newFine;
                CoarseXScroll = newCoarse;
            }
            else
            {
                FineYScroll = newFine;
                CoarseYScroll = newCoarse;
            }

            AddressLatch = !AddressLatch;
        }

        private void writeAddr(byte value)
        {
            if (!AddressLatch)
            {
                // clear and set bits 8 - 15
                TempVRAMAddress &= 0x7F << 8;
                TempVRAMAddress |= (value & 0x3F) << 8;
            }
            else
            {
                // clear lower byte
                TempVRAMAddress &= 0x0F;
                TempVRAMAddress |= value;

                VRAMAddress = TempVRAMAddress;
            }
        }

        private void incrementCoarseX()
        {
            if (CoarseXScroll == 31)
            {
                CoarseXScroll = 0;
                VRAMAddress ^= 0x0400; // switch horizontal nametable
            }
            else
                CoarseXScroll++;
        }

        private void incrementFineY()
        {
            if (FineYScroll < 7) FineYScroll++;
            else
            {
                FineYScroll = 0;
                int tempY = CoarseYScroll;

                switch (tempY)
                {
                    case 29:
                        CoarseYScroll = 0;
                        VRAMAddress ^= 0x0800; // switch vertical nametable
                        break;
                    case 31:
                        tempY = 0;
                        break;
                    default:
                        tempY++;
                        break;
                }

                CoarseYScroll = tempY;
            }
        }
    }
}