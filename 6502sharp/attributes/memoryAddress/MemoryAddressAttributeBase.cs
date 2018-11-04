using System;

namespace _6502sharp
{
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Method,
        AllowMultiple = true
    )]
    public abstract class MemoryAddressAttributeBase : Attribute
    {
        abstract public int RequiredBytes { get; }
        abstract public int Resolve(ICpu cpu, ref byte[] rawAddress);
    }
}
