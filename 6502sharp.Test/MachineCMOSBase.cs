namespace _6502sharp.Test
{
    public class MachineCMOSBase
    {
        protected IMachine machine;

        public MachineCMOSBase()
        {
            machine = new CMOSMachine();
        }
    }
}
