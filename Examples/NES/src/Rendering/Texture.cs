using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace NES.Rendering
{
    class Texture
    {
        private static float[] DEFAULT_CLAMP_COLOR = { 0, 0, 0, 0 };
        private static TextureTarget TARGET = TextureTarget.Texture2D;

        public int Id => id;

        private int id;

        public Texture(
            TextureMinFilter filtering = TextureMinFilter.Nearest,
            TextureWrapMode wrap = TextureWrapMode.ClampToBorder
        )
        {
            id = GL.GenTexture();

            Bind();

            // wrap
            GL.TexParameter(TARGET, TextureParameterName.TextureWrapS, (int)wrap);
            GL.TexParameter(TARGET, TextureParameterName.TextureWrapT, (int)wrap);
            if (wrap == TextureWrapMode.ClampToBorder)
            {
                GL.TexParameter(TARGET, TextureParameterName.TextureBorderColor, DEFAULT_CLAMP_COLOR);
            }
            // filtering
            GL.TexParameter(TARGET, TextureParameterName.TextureMinFilter, (int)filtering);
            GL.TexParameter(TARGET, TextureParameterName.TextureMagFilter, (int)filtering);

            GL.BindTexture(TARGET, 0);
        }

        public void Bind()
        {
            GL.BindTexture(TARGET, id);
        }

        public void Load(Bitmap bitmap)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
            );

            Load(data);

            bitmap.UnlockBits(data);
        }

        public void Load(BitmapData data)
        {
            Bind();

            GL.TexImage2D(
                TARGET,
                0,
                PixelInternalFormat.Rgb,
                data.Width, data.Height,
                0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0
            );
        }
    }
}