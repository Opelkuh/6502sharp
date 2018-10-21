namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        private IMachine _machine;
        private delegate void InstructionDelegate();

        public CPU(IMachine machine)
        {
            _machine = machine;

            FindInjectables();
        }
    }
}
