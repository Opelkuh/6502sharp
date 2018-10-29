using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace _6502sharp
{
    public partial class CPU
    {
        private protected void FindInjectables()
        {
            Assembly self = Assembly.GetExecutingAssembly();

            List<Assembly> assemblies = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies());

            assemblies.Remove(self);
            assemblies.Insert(0, self);

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    InjectableInstructionAttribute attribute =
                        (InjectableInstructionAttribute)type.GetCustomAttribute(typeof(InjectableInstructionAttribute), true);
                    if (attribute != null)
                    {
                        FindMethods(type);
                    }
                }
            }
        }

        private protected void FindMethods(Type classType)
        {
            // try to instantiate class
            object instance = null;
            try
            {
                instance = Activator.CreateInstance(classType, this);
            }
            catch (Exception e)
            {
                if (e is MissingMethodException)
                {
                    throw
                        new MissingMethodException($"No constructor with single parameter of type 'ICpu' found in class '{classType.Name}'. Define one or manually use ICpu.Register to register instruction");
                }
                else
                {
                    throw e;
                }
            }

            // Find CPU instructions
            MethodInfo[] methods = classType.GetMethods();
            foreach (MethodInfo method in methods)
            {
                CPUInstructionAttribute attribute = (CPUInstructionAttribute)method.GetCustomAttribute(typeof(CPUInstructionAttribute), false);
                if (attribute != null)
                {
                    // check if instruction has the same cpu type
                    if (!attribute.CPUType.HasFlag(_type)) continue;

                    // generate metadata
                    InstructionMetadata meta = new InstructionMetadata();
                    meta.ClassType = classType;
                    meta.ClassInstance = instance;
                    meta.Method = method;
                    meta.CPUAttribute = attribute;

                    FindParameters(meta);
                }
            }
        }

        private protected void FindParameters(InstructionMetadata meta)
        {
            if (meta.Parameters == null)
            {
                meta.Parameters = new List<MemoryAddressAttributeBase>();
            }

            ParameterInfo[] parameters = meta.Method.GetParameters();
            foreach (ParameterInfo parameter in parameters)
            {
                MemoryAddressAttributeBase attribute = (MemoryAddressAttributeBase)parameter.GetCustomAttribute(typeof(MemoryAddressAttributeBase), false);
                if (attribute != null)
                {
                    if (parameter.ParameterType != typeof(int))
                    {
                        throw
                            new InvalidParameterTypeException($"Memory address parameter '${parameter.Name}' of CPU instruction '{meta.ClassType.Name}.{meta.Method.Name}' must be of type 'int'");
                    }

                    // add parameter to meta
                    MemoryAddressAttributeBase param = attribute;
                    meta.Parameters.Add(param);
                }
                else
                {
                    if (parameter.ParameterType != typeof(byte))
                    {
                        throw
                            new InvalidParameterTypeException($"Parameter '${parameter.Name}' of CPU instruction '{meta.ClassType.Name}.{meta.Method.Name}' must be of type 'byte'");
                    }

                    // add parameter to meta
                    MemoryAddressAttributeBase param = null;
                    meta.Parameters.Add(param);
                }
            }

            //generate delegate
            InstructionDelegate del = GenerateDelegate(meta);

            //register delegate
            Instruction inst = new Instruction(
                meta.CPUAttribute.OpCode,
                meta.CPUAttribute.Cycles,
                del
            );

            RegisterInstruction(inst);
        }

        private protected InstructionDelegate GenerateDelegate(InstructionMetadata meta)
        {
            InstructionDelegate del = () =>
            {
                object[] instParams = new object[meta.Parameters.Count];

                for (int i = 0; i < instParams.Length; i++)
                {
                    MemoryAddressAttributeBase attr = meta.Parameters[i];
                    if (attr == null)
                    {
                        instParams[i] = FetchNext();
                    }
                    else
                    {
                        byte[] rawAddr = FetchMultiple(attr.RequiredBytes);
                        instParams[i] = attr.Resolve(this, ref rawAddr);
                    }
                }
                meta.Method.Invoke(meta.ClassInstance, instParams);
            };

            return del;
        }
    }
}