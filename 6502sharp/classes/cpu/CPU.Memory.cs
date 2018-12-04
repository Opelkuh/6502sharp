using System;
using System.Reflection;

namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public IReadable Memory => _machine.Memory;

        protected virtual byte FetchNext()
        {
            byte ret = _machine.Memory.Get(PC.Value);

            PC.Value++;

            return ret;
        }

        protected virtual byte[] FetchMultiple(int length)
        {
            byte[] ret = new byte[length];

            for (int i = 0; i < length; i++)
            {
                ret[i] = FetchNext();
            }

            return ret;
        }
    }
}