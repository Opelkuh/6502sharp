using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class LimitCPUTypeAttribute : Attribute
    {
        public CPUType Type;

        public LimitCPUTypeAttribute(CPUType type)
        {
            Type = type;
        }
    }
}
