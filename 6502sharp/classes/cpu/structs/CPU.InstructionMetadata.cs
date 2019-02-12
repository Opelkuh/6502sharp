using System;
using System.Collections.Generic;
using System.Reflection;
using _6502sharp.Reflection;

namespace _6502sharp
{
    public partial class CPU
    {
        protected struct InstructionMetadata
        {
            public Type ClassType;
            public object ClassInstance;
            public MethodInfo Method;
            public CPUInstructionAttribute CPUAttribute;
            public List<MemoryAddressAttributeBase> Parameters;
        }
    }
}