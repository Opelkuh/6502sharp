namespace _6502sharp
{
    public class CMOSMachine : DefaultMachine
    {
        public CMOSMachine() : base(CPUType.CMOS)
        {
        }

        public CMOSMachine(IReadable memory) : base(memory, CPUType.CMOS)
        {
        }
    }
}
