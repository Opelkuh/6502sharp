using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class NMOSOnlyAttribute : LimitCPUTypeAttribute
    {
        public NMOSOnlyAttribute() : base(CPUType.NMOS)
        {
        }
    }
}
