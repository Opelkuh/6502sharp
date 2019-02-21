using System.Drawing;

namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        private void fetchSpriteData(ref Sprite sprite, int y)
        {
            int bank;
            int tileOffset;
            int yOffset = y;

            if (ctrl.SpriteSize == 8)
            {
                if (sprite.FlipVertical) yOffset = 7 - y;

                bank = ctrl.SpriteAddress;
                tileOffset = sprite.TileNumber;
            }
            else
            {
                if (sprite.FlipVertical) yOffset = 15 - y;

                bank = sprite.TileNumber * 0x1000;
                tileOffset = sprite.TileNumber & 0xFE;

                if (yOffset >= 8) // adjust y if reading from second table
                {
                    tileOffset += 16;
                    yOffset -= 8;
                }
            }

            int addr = (tileOffset * 16) + bank + yOffset;
            byte lo = vram.Get(addr);
            byte hi = vram.Get(addr + 8);

            // reconstruct bits
            int[] raw = new int[8];
            for (int i = 0; i < raw.Length; i++)
            {
                int loBit = lo & 1;
                int hiBit = hi & 1;

                lo >>= 1;
                hi >>= 1;

                raw[i] = (hiBit << 1) | (loBit);
            }

            sprite.Data = getSpriteColor(ref sprite, raw);
        }

        private Color[] getSpriteColor(ref Sprite sprite, int[] raw)
        {
            Color[] ret = new Color[raw.Length];

            for (int i = 0; i < raw.Length; i++)
            {
                int addrBase;
                switch (sprite.Palette)
                {
                    case 0: addrBase = 0x3F11; break;
                    case 1: addrBase = 0x3F15; break;
                    case 2: addrBase = 0x3F19; break;
                    case 3: addrBase = 0x3F1D; break;
                    default: addrBase = 0x3F11; break;
                }

                int hex = vram.Get(addrBase + (raw[i] - 1));
                ret[i] = ColorPalette.Get(hex);
            }

            return ret;
        }
    }
}