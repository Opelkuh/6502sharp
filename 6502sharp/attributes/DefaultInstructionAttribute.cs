using System;

namespace _6502sharp.Reflection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class DefaultInstructionAttribute : Attribute
    {
        public DefaultInstructionAttribute() { }
    }
}
