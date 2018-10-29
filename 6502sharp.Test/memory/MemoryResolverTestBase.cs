namespace _6502sharp.Test.Memory
{
    public abstract class MemoryResolverTestBase
    {
        protected IMachine machine;

        public MemoryResolverTestBase()
        {
            machine = new NMOSMachine();
        }
    }
}
