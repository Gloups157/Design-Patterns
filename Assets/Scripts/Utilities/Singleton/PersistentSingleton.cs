using UnityEngine;

namespace Scripts.Utilities.Singleton
{
    /// <summary>
    /// A generic class that implements a persistent Singleton in Unity.
    /// This Singleton persists across scenes and can be unparented during Awake.
    /// </summary>
    /// <typeparam name="T">The type of the Component that the Singleton represents.</typeparam>
    public class PersistentSingleton<T> : Singleton<T> where T : Component
    {
        /// <summary>
        /// Indicates whether the Singleton should be unparented during Awake.
        /// </summary>
        public bool autoUnparentOnAwake = true;

        /// <summary>
        /// Initializes the Singleton instance.
        /// If the application is not playing, it returns immediately.
        /// If <see cref="autoUnparentOnAwake"/> is true, the GameObject will be detached from its parent.
        /// If an instance does not exist, it assigns this instance as the Singleton and ensures it is not destroyed on load.
        /// If an instance already exists and it is not this one, the GameObject is destroyed.
        /// </summary>
        protected override void InitializeSingleton()
        {
            if (!Application.isPlaying) return;
            if (autoUnparentOnAwake) transform.SetParent(null);
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this) Destroy(gameObject);
        }
    }
}