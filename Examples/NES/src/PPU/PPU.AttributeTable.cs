namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        private int getBackgroundPalette()
        {
            int palettePosition = ((VRAMAddress.Value >> 4) & 4) | (VRAMAddress.Value & 2);

            int data = vram.Get(VRAMAddress.AttributeAddress);
            return (data >> palettePosition) & 3;
        }
    }
}