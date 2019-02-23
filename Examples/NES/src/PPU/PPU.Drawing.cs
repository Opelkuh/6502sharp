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

            Color backgroundPixel =
                backgroundData != null ? backgroundData[FineXScroll] : Color.Transparent;

            Sprite? sprite = getCurrentSprite();
            Color spritePixel;
            if (sprite.HasValue)
            {
                int index = sprite.Value.X - renderX;
                spritePixel = sprite.Value.Data[index];
            }
            else spritePixel = Color.Transparent;

            if (renderX < 8)
            {
                if (mask.ShowBackgroundLeftmost) backgroundPixel = Color.Transparent;
                if (mask.ShowSpritesLeftmost) spritePixel = Color.Transparent;
            }

            Color toDraw = ColorPalette.Get(vram.Get(0x3F00));

            if (backgroundPixel.A != 0) toDraw = backgroundPixel;

            if (sprite.HasValue)
            {
                Sprite spr = sprite.Value;
                if (!spr.BehindBackground)
                {
                    if (spr.Index == 0 && toDraw.A != 0 && spritePixel.A != 0)
                    {
                        status.Sprite0Hit = true;
                    }

                    toDraw = spritePixel;
                }
            }

            // draw
            lock (Canvas)
            {
                Canvas.SetPixelGL(renderX, Scanline, toDraw);
            }

        }

        private Sprite? getCurrentSprite()
        {
            int renderX = Cycles - 1;

            if (scanlineSprites == null) return null;

            for (int i = 0; i < scanlineSprites.Length; i++)
            {
                if (!scanlineSprites[i].HasValue) continue;

                Sprite spr = scanlineSprites[i].Value;

                if (!Range.Fits(renderX, renderX + (spr.Data.Length - 1), spr.X)) continue;

                return spr;
            }

            return null;
        }
    }
}