using System;

namespace NES
{
    interface IMapper : IDisposable
    {
        byte? Get(int address);
        bool Set(int address, byte value);
    }
}