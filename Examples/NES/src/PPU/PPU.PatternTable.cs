using System.Drawing;
using System.Linq;

namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        private static readonly int[] BACKGROUND_PALETTE_ADDR = {
            0x3F01,
            0x3F05,
            0x3F09,
            0x3F0D,
        };

        private static readonly int[] SPRITE_PALETTE_ADDR = {
            0x3F11,
            0x3F15,
            0x3F19,
            0x3F1D,
        };

        private int getBackgroundPatternAddress()
        {
            return (nametableRaw * 16) + ctrl.BackgroundAddress + VRAMAddress.FineYScroll;
        }

        private Color[] fetchBackgroundData()
        {
            byte lo = vram.Get(backgroundLo);
            byte hi = vram.Get(backgroundHi);

            int[] raw = reconstructPatternBits(lo, hi);

            return getColor(BACKGROUND_PALETTE_ADDR, backgroundPalette, raw);
        }

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
            int[] raw = reconstructPatternBits(lo, hi);

            if (sprite.FlipHorizontal) raw.Reverse();

            sprite.Data = getColor(SPRITE_PALETTE_ADDR, sprite.Palette, raw);
        }

        private Color[] getColor(int[] paletteAddr, int palette, int[] raw)
        {
            Color[] ret = new Color[raw.Length];

            for (int i = 0; i < raw.Length; i++)
            {
                int addrBase = paletteAddr[palette];

                int target = raw[i] - 1;
                if (target >= 0)
                {
                    int hex = vram.Get(addrBase + target);
                    ret[i] = ColorPalette.Get(hex);
                }
                else ret[i] = Color.Transparent;
            }

            return ret;
        }

        private int[] reconstructPatternBits(byte lo, byte hi)
        {
            int[] ret = new int[8];
            for (int i = 0; i < ret.Length; i++)
            {
                int loBit = lo & 1;
                int hiBit = hi & 1;

                lo >>= 1;
                hi >>= 1;

                ret[i] = (hiBit << 1) | (loBit);
            }

            return ret;
        }
    }
}