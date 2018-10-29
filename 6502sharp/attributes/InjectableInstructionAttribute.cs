using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InjectableInstructionAttribute : Attribute
    {
        public InjectableInstructionAttribute() { }
    }
}
