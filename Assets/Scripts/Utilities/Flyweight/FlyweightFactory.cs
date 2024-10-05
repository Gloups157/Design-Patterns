using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Scripts.Utilities.Flyweight
{
    /// <summary>
    /// FlyweightFactory is responsible for managing and creating object pools for Flyweight instances.
    /// This class uses the UnityEngine.Pool.IObjectPool interface to efficiently handle 
    /// object pooling, reducing memory allocations and improving performance.
    /// </summary>
    public class FlyweightFactory : MonoBehaviour
    {
        /// <summary>
        /// Specifies whether the pool should perform collection checks.
        /// </summary>
        [SerializeField] bool collectionCheck = true;

        /// <summary>
        /// The default capacity of the pool.
        /// </summary>
        [SerializeField] int defaultCapacity = 10;

        /// <summary>
        /// The maximum number of objects that can be held in the pool.
        /// </summary>
        [SerializeField] int maxPoolSize = 100;

        /// <summary>
        /// Dictionary mapping Flyweight instances to their corresponding object pools.
        /// </summary>
        readonly Dictionary<Flyweight, IObjectPool<Flyweight>> poolMap = new();

        /// <summary>
        /// Spawns a Flyweight instance based on the specified settings.
        /// </summary>
        /// <param name="settings">The settings used to configure the Flyweight instance.</param>
        /// <returns>A Flyweight instance from the corresponding pool.</returns>
        public Flyweight Spawn(FlyweightSettings settings) => GetPoolFor(settings)?.Get();

        /// <summary>
        /// Returns a Flyweight instance to its corresponding pool.
        /// </summary>
        /// <param name="flyweight">The Flyweight instance to return to the pool.</param>
        public void ReturnToPool(Flyweight flyweight) => GetPoolFor(flyweight.settings)?.Release(flyweight);

        /// <summary>
        /// Retrieves or creates an object pool for the specified Flyweight settings.
        /// </summary>
        /// <param name="settings">The settings used to configure the Flyweight instance.</param>
        /// <returns>The object pool associated with the given settings.</returns>
        public IObjectPool<Flyweight> GetPoolFor(FlyweightSettings settings)
        {
            IObjectPool<Flyweight> pool;
            if (poolMap.TryGetValue(settings.prefab.GetComponent<Flyweight>(), out pool)) return pool;

            pool = new ObjectPool<Flyweight>
            (
                settings.Create,
                settings.OnGet,
                settings.OnRelease,
                settings.OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize
            );

            poolMap.Add(settings.prefab.GetComponent<Flyweight>(), pool);
            return pool;
        }
    }
}