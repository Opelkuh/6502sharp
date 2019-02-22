using System.Drawing;
using NES.Rendering;

namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        public Bitmap Canvas = new Bitmap(256, 240);

        private void nextPixel()
        {
            int renderX = Cycles - 1;

            Color backgroundPixel = backgroundData[FineXScroll];

            Sprite? sprite = getCurrentSprite();
            Color spritePixel =
                sprite.HasValue ? sprite.Value.Data[renderX - sprite.Value.X] : Color.Transparent;

            if (renderX < 8)
            {
                if (mask.ShowBackgroundLeftmost) backgroundPixel = Color.Transparent;
                if (mask.ShowSpritesLeftmost) spritePixel = Color.Transparent;
            }

            Color toDraw = ColorPalette.Get(vram.Get(0x3F00));

            if (backgroundPixel != Color.Transparent) toDraw = backgroundPixel;

            if (sprite.HasValue)
            {
                Sprite spr = sprite.Value;
                if (!spr.BehindBackground)
                {
                    if (spr.Index == 0 && toDraw != Color.Transparent && spritePixel != Color.Transparent)
                    {
                        status.Sprite0Hit = true;
                    }

                    toDraw = spritePixel;
                }
            }

            // draw
            Canvas.SetPixelGL(renderX, Scanline, toDraw);
        }

        private Sprite? getCurrentSprite()
        {
            int renderX = Cycles - 1;

            for (int i = 0; i < scanlineSprites.Length; i++)
            {
                if (!scanlineSprites[i].HasValue) continue;

                Sprite spr = scanlineSprites[i].Value;

                if (!Range.Fits(renderX, renderX + spr.Data.Length, spr.X)) continue;

                return spr;
            }

            return null;
        }
    }
}