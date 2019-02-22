namespace NES.PPU
{
    partial class PictureProcessingUnit
    {
        public int SleepCycles;

        private int Scanline;
        private int Frame;

        #region Background registers
        private byte nametableByte;
        private int backgroundPalette;
        private byte backgroundLo;
        private byte backgroundHi;
        #endregion
    }
}