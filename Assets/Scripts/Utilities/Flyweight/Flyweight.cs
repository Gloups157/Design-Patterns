using UnityEngine;

namespace Scripts.Utilities.Flyweight
{
    /// <summary>
    /// Represents a Flyweight in the Unity context.
    /// This class holds a reference to a FlyweightSettings object,
    /// which contains the shared state that can be used across multiple Flyweight instances.
    /// </summary>
    public class Flyweight : MonoBehaviour
    {
        /// <summary>
        /// The settings associated with this Flyweight.
        /// These settings represent the shared state that can be reused among multiple Flyweight objects.
        /// </summary>
        public FlyweightSettings settings;
    }
}