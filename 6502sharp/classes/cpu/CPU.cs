namespace _6502sharp
{
    public partial class CPU
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
