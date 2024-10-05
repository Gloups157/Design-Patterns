using System.Collections.Generic;
using System.Reflection;
using System;

namespace Scripts.Utilities.EventBus
{
    /// <summary>
    /// A utility class for retrieving types from predefined assemblies in the current application domain.
    /// </summary>
    public static class PredefinedAssemblyUtil
    {
        /// <summary>
        /// An enumeration representing different types of assemblies in a Unity project.
        /// </summary>
        enum AssemblyType
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpEditorFirstPass,
            AssemblyCSharpFirstPass
        }

        /// <summary>
        /// Determines the type of assembly based on its name.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <returns>The corresponding <see cref="AssemblyType"/> if found; otherwise, null.</returns>
        static AssemblyType? GetAssemblyType(string assemblyName)
        {
            return assemblyName switch
            {
                "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
                "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
                "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
                "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
                _ => null
            };
        }

        /// <summary>
        /// Adds types from an assembly to a collection if they implement a specified interface.
        /// </summary>
        /// <param name="assembly">An array of types in the assembly.</param>
        /// <param name="types">The collection to which the types will be added.</param>
        /// <param name="interfaceType">The interface type that the types must implement.</param>
        static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
        {
            if (assembly == null) return;
            for (int i = 0; i < assembly.Length; i++)
            {
                var type = assembly[i];
                if (type != interfaceType && interfaceType.IsAssignableFrom(type)) types.Add(type);
            }
        }

        /// <summary>
        /// Retrieves a list of types from predefined assemblies that implement a specified interface.
        /// </summary>
        /// <param name="interfaceType">The interface type that the returned types must implement.</param>
        /// <returns>A list of types that implement the specified interface.</returns>
        public static List<Type> GetTypes(Type interfaceType)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<AssemblyType, Type[]> assemblyTypeMap = new();
            List<Type> typeList = new();
            for (int i = 0; i < assemblies.Length; i++)
            {
                AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
                if (assemblyType != null) assemblyTypeMap.Add((AssemblyType) assemblyType, assemblies[i].GetTypes());
            }
            assemblyTypeMap.TryGetValue(AssemblyType.AssemblyCSharp, out var assemblyCSharpTypes);
            AddTypesFromAssembly(assemblyCSharpTypes, typeList, interfaceType);
            assemblyTypeMap.TryGetValue(AssemblyType.AssemblyCSharpFirstPass, out var assemblyCSharpFirstPassTypes);
            AddTypesFromAssembly(assemblyCSharpFirstPassTypes, typeList, interfaceType);
            return typeList;
        }
    }
}