using System;

namespace _6502sharp.Reflection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InjectableInstructionAttribute : Attribute
    {
        public InjectableInstructionAttribute() { }
    }
}
