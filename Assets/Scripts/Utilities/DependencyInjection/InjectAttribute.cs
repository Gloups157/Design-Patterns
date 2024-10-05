using System;

namespace Scripts.Utilities.DependencyInjection
{
    /// <summary>
    /// Attribute used to mark fields or methods for dependency injection.
    /// This attribute should be applied to fields or methods that require
    /// automatic injection of dependencies.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute { }
}