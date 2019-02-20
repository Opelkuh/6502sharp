namespace NES.PPU
{
    abstract class PPURegisterBase
    {
        public byte Value
        {
            get => store;
            set => store = value;
        }

        protected byte store = 0;

        protected bool HasFlag(byte flag)
        {
            return (store & flag) > 0;
        }

        protected void SetFlag(int flag, bool value)
        {
            if (value) store |= (byte)flag;
            else store &= (byte)(~flag);
        }
    }
}