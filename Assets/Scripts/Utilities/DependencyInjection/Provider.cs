using UnityEngine;

namespace Scripts.Utilities.DependencyInjection
{
    /// <summary>
    /// Abstract class that represents a provider for dependency injection in Unity.
    /// Implements the <see cref="IDependencyProvider"/> interface.
    /// </summary>
    public abstract class Provider : MonoBehaviour, IDependencyProvider { }
}