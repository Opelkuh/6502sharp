using System;

namespace _6502sharp.Reflection
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
