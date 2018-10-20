using System;
using System.Reflection;

namespace _6502sharp
{
    public partial class CPU
    {
        protected class Reflector
        {
            internal void FindInjectables()
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

            internal void FindMethods(Type classType)
            {
                // try to instantiate class
                object instance = null;
                try
                {
                    instance = Activator.CreateInstance(classType);
                }
                catch (Exception e)
                {
                    if (e is MissingMethodException)
                    {
                        throw
                            new MissingMethodException($"No 0 parameter constructor found in class '{classType.Name}'. Define one or manually use Machine.Register to register it");
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
                    object attribute = method.GetCustomAttribute(typeof(CPUInstructionAttribute), false);
                    if (attribute != null)
                    {
                        FindParameters(classType, instance, method);
                    }
                }
            }

            internal void FindParameters(Type classType, object instance, MethodInfo method)
            {
                ParameterInfo[] parameters = method.GetParameters();
                foreach (ParameterInfo parameter in parameters)
                {
                    MemoryAddressAttributeBase attribute = (MemoryAddressAttributeBase)parameter.GetCustomAttribute(typeof(MemoryAddressAttributeBase), false);
                    if (attribute != null)
                    {
                        if (parameter.ParameterType != typeof(int))
                        {
                            throw
                                new InvalidParameterTypeException($"Memory address parameter of CPU instruction '{classType.Name}.{method.Name}' must be of type 'int'");
                        }
                    }
                    else
                    {
                        if (parameter.ParameterType != typeof(byte))
                        {
                            throw
                                new InvalidParameterTypeException($"Parameter of CPU instruction '{classType.Name}.{method.Name}' must be of type 'byte'");
                        }
                    }
                }
            }
        }
    }
}
