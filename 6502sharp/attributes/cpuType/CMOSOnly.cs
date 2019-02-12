using System;

namespace _6502sharp.Reflection
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class CMOSOnlyAttribute : LimitCPUTypeAttribute
    {
        public CMOSOnlyAttribute() : base(CPUType.CMOS)
        {
        }
    }
}
