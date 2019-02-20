using System.Drawing;

namespace NES.Rendering
{
    public static class BitmapExtensions
    {
        public static void SetPixelGL(this Bitmap bitmap, int x, int y, Color color)
        {
            int offset = bitmap.Height - 1;
            bitmap.SetPixel(x, offset - y, color);
        }
    }
}