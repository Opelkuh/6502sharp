using System.Drawing;

namespace NES.PPU
{
    public struct Sprite
    {
        public Color[] Data;
        public int X;
        public int Y;
        public int Index;
        public int TileNumber;
        public int Palette;
        public bool FlipVertical;
        public bool FlipHorizontal;
        public bool BehindBackground;
    }
}