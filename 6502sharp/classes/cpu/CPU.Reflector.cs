using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace _6502sharp
{
    public partial class CPU
    {
        private Assembly _libAssembly = Assembly.GetExecutingAssembly();

        private protected void FindDefaultInjectables()
        {
            findByAttribute(_libAssembly, typeof(DefaultInstructionAttribute));
        }

        private protected void FindInjectables()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                findByAttribute(assembly, typeof(InjectableInstructionAttribute));
            }
        }

        private void findByAttribute(Assembly assembly, Type type)
        {
            foreach (Type t in assembly.GetTypes())
            {
                Attribute attribute = t.GetCustomAttribute(type, true);
                if (attribute != null)
                {
                    FindMethods(t);
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
                CPUInstructionAttribute[] attributes = (CPUInstructionAttribute[])method.GetCustomAttributes(typeof(CPUInstructionAttribute), false);

                if (attributes.Length < 1) continue;

                List<InstructionMetadata?> metadata = new List<InstructionMetadata?>();

                foreach (var attribute in attributes)
                {
                    // check if instruction has the same cpu type
                    if (!attribute.CPUType.HasFlag(_type))
                    {
                        metadata.Add(null);
                        continue;
                    }

                    // generate metadata
                    InstructionMetadata meta = new InstructionMetadata();
                    meta.ClassType = classType;
                    meta.ClassInstance = instance;
                    meta.Method = method;
                    meta.CPUAttribute = attribute;

                    metadata.Add(meta);
                }

                if (metadata.Count == 1)
                {
                    if (metadata[0].HasValue) FindParameters(metadata[0].Value);
                }
                else if (metadata.Count > 1)
                {
                    FindMemoryAddressParameters(method, metadata.ToArray());
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
                        ThrowInvalidParameterType(parameter.Name, "int", meta);

                    // add parameter to meta
                    MemoryAddressAttributeBase param = attribute;
                    meta.Parameters.Add(param);
                }
                else
                {
                    if (parameter.ParameterType != typeof(byte))
                        ThrowInvalidParameterType(parameter.Name, "byte", meta);

                    // add parameter to meta
                    MemoryAddressAttributeBase param = null;
                    meta.Parameters.Add(param);
                }
            }

            RegisterInstructionMetadata(meta);
        }

        private protected void FindMemoryAddressParameters(MethodInfo method, InstructionMetadata?[] meta)
        {
            if (meta.Length < 1) return;
            // check if all attributes have memory attribute
            MemoryAddressAttributeBase[] memAttributes =
                (MemoryAddressAttributeBase[])method.GetCustomAttributes(typeof(MemoryAddressAttributeBase), false);

            if (memAttributes.Length > 0 && meta.Length != memAttributes.Length)
                throw new Exception($"Method {method.Name} doesn't have the same number of CPUInstruction attributes ({meta.Length}) and MemoryResolver attributes ({memAttributes.Length})!");

            // check parameters
            ParameterInfo[] parameters = method.GetParameters();
            bool[] isIntParam = new bool[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                if (parameter.ParameterType == typeof(int))
                {
                    isIntParam[i] = true;
                    continue;
                }
                else if (parameter.ParameterType == typeof(byte))
                {
                    isIntParam[i] = false;
                    continue;
                }
                else
                {
                    foreach (InstructionMetadata? metadata in meta)
                    {
                        if (metadata.HasValue)
                        {
                            ThrowInvalidParameterType(parameter.Name, "int|byte", metadata.Value);
                        }
                    }
                    // in case that all provided metadata is null
                    return;
                }
            }

            // generate delegates
            for (int i = 0; i < meta.Length; i++)
            {
                if (!meta[i].HasValue) continue;
                InstructionMetadata metadata = meta[i].Value;

                metadata.Parameters = new List<MemoryAddressAttributeBase>();

                foreach (bool isInt in isIntParam)
                {
                    if (isInt) metadata.Parameters.Add(memAttributes[i]);
                    else metadata.Parameters.Add(null);
                }

                RegisterInstructionMetadata(metadata);
            }
        }

        private protected void RegisterInstructionMetadata(InstructionMetadata meta)
        {
            //generate delegate
            Action del = GenerateDelegate(meta);

            //register delegate
            Instruction inst = new Instruction(
                meta.CPUAttribute.OpCode,
                meta.CPUAttribute.Cycles,
                del
            );

            RegisterInstruction(inst);
        }

        private protected Action GenerateDelegate(InstructionMetadata meta)
        {
            Action del = () =>
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

        private void ThrowInvalidParameterType(string parameterName, string targetType, InstructionMetadata meta)
        {
            throw
                new InvalidParameterTypeException($"Parameter '{parameterName}' of CPU instruction '{meta.ClassType.Name}.{meta.Method.Name}' must be of type '{targetType}'");
        }
    }
}