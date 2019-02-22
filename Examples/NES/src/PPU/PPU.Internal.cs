namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        #region Internal registers
        // OAM
        private byte OAMAddress = 0;

        // Scrolling
        private ScrollAddress VRAMAddress = new ScrollAddress();
        private ScrollAddress TempVRAMAddress = new ScrollAddress();
        private int FineXScroll = 0;
        private bool AddressLatch = false;

        private byte ReadBuffer = 0;
        #endregion

        private void writeScroll(byte value)
        {
            int newFine = value >> 3;
            int newCoarse = value & 0x07;

            if (!AddressLatch)
            {
                FineXScroll = newFine;
                TempVRAMAddress.CoarseXScroll = newCoarse;
            }
            else
            {
                TempVRAMAddress.FineYScroll = newFine;
                TempVRAMAddress.CoarseYScroll = newCoarse;
            }

            AddressLatch = !AddressLatch;
        }

        private void writeAddr(byte value)
        {
            if (!AddressLatch)
            {
                // clear and set bits 8 - 15
                TempVRAMAddress.Value &= 0x7F << 8;
                TempVRAMAddress.Value |= (value & 0x3F) << 8;
            }
            else
            {
                // clear lower byte
                TempVRAMAddress.Value &= 0x0F;
                TempVRAMAddress.Value |= value;

                VRAMAddress.Value = TempVRAMAddress.Value;
            }
        }

        private void incrementCoarseX()
        {
            if (VRAMAddress.CoarseXScroll == 31)
            {
                VRAMAddress.CoarseXScroll = 0;
                VRAMAddress.Value ^= 0x0400; // switch horizontal nametable
            }
            else
                VRAMAddress.CoarseXScroll++;
        }

        private void incrementFineY()
        {
            if (VRAMAddress.FineYScroll < 7) VRAMAddress.FineYScroll++;
            else
            {
                VRAMAddress.FineYScroll = 0;
                int tempY = VRAMAddress.CoarseYScroll;

                switch (tempY)
                {
                    case 29:
                        VRAMAddress.CoarseYScroll = 0;
                        VRAMAddress.Value ^= 0x0800; // switch vertical nametable
                        break;
                    case 31:
                        tempY = 0;
                        break;
                    default:
                        tempY++;
                        break;
                }

                VRAMAddress.CoarseYScroll = tempY;
            }
        }
    }
}