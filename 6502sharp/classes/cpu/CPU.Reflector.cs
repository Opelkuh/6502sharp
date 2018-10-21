using System;
using System.Reflection;

namespace _6502sharp
{
    public partial class CPU
    {
        private protected void FindInjectables()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
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
                instance = Activator.CreateInstance(classType, _machine);
            }
            catch (Exception e)
            {
                if (e is MissingMethodException)
                {
                    throw
                        new MissingMethodException($"No constructor with parameter of type 'IMachine' found in class '{classType.Name}'. Define one or manually use Machine.Register to register instruction");
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
                meta.Parameters = new System.Collections.Generic.List<Tuple<int, MemoryAddressAttributeBase>>();
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
                    Tuple<int, MemoryAddressAttributeBase> param = new Tuple<int, MemoryAddressAttributeBase>(parameter.Position, attribute);
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
                    Tuple<int, MemoryAddressAttributeBase> param = new Tuple<int, MemoryAddressAttributeBase>(parameter.Position, null);
                    meta.Parameters.Add(param);
                }
            }
        }
    }
}