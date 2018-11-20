using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class DefaultInstructionAttribute : Attribute
    {
        public DefaultInstructionAttribute() { }
    }
}
