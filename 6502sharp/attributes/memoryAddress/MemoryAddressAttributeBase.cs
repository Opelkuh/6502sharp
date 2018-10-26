using System;

namespace _6502sharp
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public abstract class MemoryAddressAttributeBase : Attribute
    {
        abstract public int RequiredBytes { get; }
        abstract public int Resolve(ICpu cpu, ref byte[] rawAddress);
    }
}
