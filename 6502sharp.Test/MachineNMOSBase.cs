using System;
using Xunit;

namespace _6502sharp.Test
{
    public class MachineNMOSBase
    {
        protected NMOSMachine machine;

        public MachineNMOSBase()
        {
            machine = new NMOSMachine();
        }
    }
}
