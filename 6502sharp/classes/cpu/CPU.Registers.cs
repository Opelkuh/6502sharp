namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public IStatusRegister SR { get => _sr; set => _sr = value; }

        public IRegister8Bit A { get => _a; set => _a = value; }

        public IRegister8Bit X { get => _x; set => _x = value; }

        public IRegister8Bit Y { get => _y; set => _y = value; }

        public IRegister8Bit SP { get => _sp; set => _sp = value; }

        public IRegister16Bit PC { get => _pc; set => _pc = value; }


        private IStatusRegister _sr = new StatusRegister();
        private IRegister8Bit _a = new Register();
        private IRegister8Bit _x = new Register();
        private IRegister8Bit _y = new Register();
        private IRegister8Bit _sp = new Register();
        private IRegister16Bit _pc = new Register16Bit();
    }
}
