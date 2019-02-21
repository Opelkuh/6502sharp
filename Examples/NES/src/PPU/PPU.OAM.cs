namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        private Sprite?[] prepareSprites()
        {
            Sprite?[] ret = new Sprite?[8];
            int found = 0;
            for (int i = 0; i < 256; i += 4)
            {
                Sprite sprite = getSpriteInfo(i);

                int relativeY = Scanline - sprite.Y;
                if (!Range.Fits(0, ctrl.SpriteSize - 1, relativeY))
                    continue;

                found++;
                if (found > 8)
                {
                    status.SpriteOverflow = true;
                    break;
                }

                fetchSpriteData(ref sprite, relativeY);
                ret[found] = sprite;
            }

            return ret;
        }

        private Sprite getSpriteInfo(int location)
        {
            Sprite ret = new Sprite();

            ret.Y = OAM.Get(location + 0);
            ret.TileNumber = OAM.Get(location + 1);
            ret.Palette = OAM.Get(location + 2) & 3;
            ret.BehindBackground = (OAM.Get(location + 2) & (1 << 5)) > 0;
            ret.FlipHorizontal = (OAM.Get(location + 2) & (1 << 6)) > 0;
            ret.FlipVertical = (OAM.Get(location + 2) & (1 << 7)) > 0;
            ret.X = OAM.Get(location + 3);

            return ret;
        }

        private void startDMA(byte hiByte)
        {
            // add sleep cycles to CPU
            mach.CPU.SleepCycles += 513;

            // add extra cycle if FinishedCycles is odd after transfer
            int total = mach.CPU.FinishedCycles + mach.CPU.SleepCycles;
            if ((total & 1) == 0) mach.CPU.SleepCycles++;

            // DMA transfer
            int start = hiByte << 8;
            int end = start | 0x00FF;
            for (int i = start; i <= end; i++)
            {
                byte val = mach.Memory.Get(i);
                OAM.Set(OAMAddress++, val);
            }
        }
    }
}