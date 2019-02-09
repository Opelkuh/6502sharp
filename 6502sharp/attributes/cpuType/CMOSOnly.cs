using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class CMOSOnlyAttribute : LimitCPUTypeAttribute
    {
        public CMOSOnlyAttribute() : base(CPUType.CMOS)
        {
        }
    }
}
