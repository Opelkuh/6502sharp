namespace _6502sharp
{
    public class NMOSMachine : DefaultMachine
    {
        public NMOSMachine() : base(CPUType.NMOS)
        {
        }

        public NMOSMachine(IReadable memory) : base(memory, CPUType.NMOS)
        {
        }
    }
}
