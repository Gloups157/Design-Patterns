using System;

namespace Scripts.Utilities.DependencyInjection
{
    /// <summary>
    /// Attribute used to mark a method as a provider of dependencies.
    /// This attribute should be applied to methods that provide a specific
    /// type of service or resource for dependency injection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : Attribute { }
}