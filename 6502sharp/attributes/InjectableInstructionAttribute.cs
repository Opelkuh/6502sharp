using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InjectableInstructionAttribute : Attribute
    {
        public string[] Tags = new string[0];

        public InjectableInstructionAttribute() { }
        public InjectableInstructionAttribute(params string[] tags)
        {
            Tags = tags;
        }
    }
}
