using System;
using System.IO;
using System.Drawing;

namespace NES.PPU
{
    public static class ColorPalette
    {
        private const string PALETTE_PATH = @"palette.pal";
        private const int COLORS = 64;

        private static Color[] colors = new Color[COLORS];
        private static bool isReady = false;
        public static void Initialize(string path = PALETTE_PATH)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                for (int i = 0; i < colors.Length; i++)
                {
                    byte[] data = reader.ReadBytes(3);

                    if (data.Length != 3)
                        throw new Exception($"Invalid palette file! Palette file has to have atleast {COLORS * 3} bytes.");

                    colors[i] = Color.FromArgb(data[0], data[1], data[2]);
                }
            }

            isReady = true;
        }

        public static Color Get(int index)
        {
            if (!isReady) Initialize();
            return colors[index % COLORS];
        }
    }
}