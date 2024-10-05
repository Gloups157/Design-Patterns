using UnityEngine;

namespace Scripts.Utilities.Singleton
{
    /// <summary>
    /// A generic singleton class for Unity components.
    /// </summary>
    /// <typeparam name="T">The type of the singleton component.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// The singleton instance of the component.
        /// </summary>
        protected static T instance;

        /// <summary>
        /// Tries to get the current instance of the singleton.
        /// </summary>
        /// <returns>
        /// The instance of the singleton if it exists; otherwise, <c>null</c>.
        /// </returns>
        public static T TryGetInstance() => instance ?? null;

        /// <summary>
        /// Gets the singleton instance of the component. If the instance is not found,
        /// it searches for an existing instance or creates a new one if necessary.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        GameObject gameObject = new GameObject(typeof(T).Name);
                        instance = gameObject.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Unity's Awake method, used to initialize the singleton instance.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        /// <summary>
        /// Initializes the singleton instance if the application is running.
        /// </summary>
        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying) return;
            instance = this as T;
        }
    }
}