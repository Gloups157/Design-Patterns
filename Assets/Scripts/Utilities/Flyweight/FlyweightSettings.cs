using UnityEngine;

namespace Scripts.Utilities.Flyweight
{
    /// <summary>
    /// Represents the settings for a Flyweight object, implemented as a ScriptableObject in Unity.
    /// This class defines how Flyweight objects are created, activated, deactivated, and destroyed.
    /// </summary>
    public class FlyweightSettings : ScriptableObject
    {
        /// <summary>
        /// The prefab used to create instances of the Flyweight object.
        /// </summary>
        public GameObject prefab;

        /// <summary>
        /// Creates a new Flyweight instance based on the specified prefab.
        /// The new GameObject is initially inactive and configured with this FlyweightSettings instance.
        /// </summary>
        /// <returns>The newly created Flyweight instance.</returns>
        public virtual Flyweight Create()
        {
            var gameObject = Instantiate(prefab);
            gameObject.SetActive(false);
            gameObject.name = prefab.name;
            var flyweight = gameObject.GetComponent<Flyweight>();
            flyweight.settings = this;
            return flyweight;
        }

        /// <summary>
        /// Activates the Flyweight object when it is retrieved from a pool or when needed.
        /// </summary>
        /// <param name="flyweight">The Flyweight instance to activate.</param>
        public virtual void OnGet(Flyweight flyweight) => flyweight.gameObject.SetActive(true);

        /// <summary>
        /// Deactivates the Flyweight object, typically when it is returned to a pool.
        /// </summary>
        /// <param name="flyweight">The Flyweight instance to deactivate.</param>
        public virtual void OnRelease(Flyweight flyweight) => flyweight.gameObject.SetActive(false);

        /// <summary>
        /// Destroys the Flyweight object when it is no longer needed or when removed from a pool.
        /// </summary>
        /// <param name="flyweight">The Flyweight instance to destroy.</param>
        public virtual void OnDestroyPoolObject(Flyweight flyweight) => Destroy(flyweight.gameObject);
    }
}