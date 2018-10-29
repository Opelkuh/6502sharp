namespace _6502sharp.Test.Memory
{
    public abstract class MemoryResolverCMOSTestBase
    {
        protected IMachine machine;

        public MemoryResolverCMOSTestBase()
        {
            machine = new CMOSMachine();
        }
    }
}
