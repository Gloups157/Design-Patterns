using Scripts.Utilities.Singleton;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace Scripts.Utilities.DependencyInjection
{
    /// <summary>
    /// A Singleton MonoBehaviour responsible for managing dependency injection.
    /// The <see cref="Injector"/> class scans for <see cref="IDependencyProvider"/> 
    /// implementations and injects dependencies into fields, properties, and methods 
    /// marked with the <see cref="InjectAttribute"/>.
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public class Injector : Singleton<Injector>
    {
        /// <summary>
        /// The binding flags used to search for members in a class.
        /// It includes instance, public, and non-public members.
        /// </summary>
        const BindingFlags BINDINGFLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// A dictionary that maps types to their corresponding instances provided by the <see cref="IDependencyProvider"/>.
        /// </summary>
        readonly Dictionary<Type, object> registryMap = new();

        /// <summary>
        /// Initializes the <see cref="Injector"/> by finding and registering all dependency providers
        /// and injecting dependencies into all injectables.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            var providers = FindMonoBehaviours().OfType<IDependencyProvider>();
            foreach (var provider in providers)
            {
                RegistryProvider(provider);
            }
            var injectables = FindMonoBehaviours().Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
        }

        /// <summary>
        /// Determines if a MonoBehaviour contains any members marked with the <see cref="InjectAttribute"/>.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour to check.</param>
        /// <returns>True if the MonoBehaviour contains injectable members, otherwise false.</returns>
        static bool IsInjectable(MonoBehaviour monoBehaviour)
        {
            var members = monoBehaviour.GetType().GetMembers(BINDINGFLAGS);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }

        /// <summary>
        /// Resolves an instance of the specified type from the registry.
        /// </summary>
        /// <param name="type">The type to resolve.</param>
        /// <returns>The resolved instance, or null if not found.</returns>
        object Resolve(Type type)
        {
            registryMap.TryGetValue(type, out var resolvedInstance);
            return resolvedInstance;
        }

        /// <summary>
        /// Injects dependencies into the fields, properties, and methods of the given instance.
        /// </summary>
        /// <param name="instance">The object instance to inject dependencies into.</param>
        /// <exception cref="Exception">Thrown when a dependency cannot be resolved or injected.</exception>
        void Inject(object instance)
        {
            var type = instance.GetType();
            var injectableFields = type.GetFields(BINDINGFLAGS).Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
            foreach (var injectableField in injectableFields)
            {
                var fieldType = injectableField.FieldType;
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null) throw new Exception($"Failed to inject dependency into field '{injectableField.Name}' of class '{type.Name}'.");
                injectableField.SetValue(instance, resolvedInstance);
            }

            var injectableMethods = type.GetMethods(BINDINGFLAGS).Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
            foreach (var injectableMethod in injectableMethods)
            {
                var requiredParameters = injectableMethod.GetParameters().Select(parameter => parameter.ParameterType).ToArray();
                var resolvedInstances = requiredParameters.Select(Resolve).ToArray();
                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null)) throw new Exception($"Failed to inject dependencies into method '{injectableMethod.Name}' of class '{type.Name}'.");
                injectableMethod.Invoke(instance, resolvedInstances);
            }

            var injectableProperties = type.GetProperties(BINDINGFLAGS).Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
            foreach (var injectableProperty in injectableProperties)
            {
                var propertyType = injectableProperty.PropertyType;
                var resolvedInstance = Resolve(propertyType);
                if (resolvedInstance == null) throw new Exception($"Failed to inject dependency into property '{injectableProperty.Name}' of class '{type.Name}'.");
                injectableProperty.SetValue(instance, resolvedInstance);
            }
        }

        /// <summary>
        /// Registers an <see cref="IDependencyProvider"/> by invoking its methods marked with the <see cref="ProvideAttribute"/>
        /// and storing the returned instances in the registry.
        /// </summary>
        /// <param name="provider">The dependency provider to register.</param>
        /// <exception cref="Exception">Thrown when a provider method returns null.</exception>
        void RegistryProvider(IDependencyProvider provider)
        {
            var methods = provider.GetType().GetMethods(BINDINGFLAGS);
            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;
                var returnType = method.ReturnType;
                var provideInstance = method.Invoke(provider, null);
                if (provideInstance != null)
                    registryMap.Add(returnType, provideInstance);
                else throw new Exception($"Provider '{provider.GetType().Name}' returned null for '{returnType.Name}'");
            }
        }

        /// <summary>
        /// Finds all MonoBehaviour instances in the scene.
        /// </summary>
        /// <returns>An array of all MonoBehaviour instances.</returns>
        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
        }
    }
}