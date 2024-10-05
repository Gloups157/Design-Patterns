namespace UnityEngine
{
    /// <summary>
    /// Provides extension methods for the GameObject class to facilitate component management and child object destruction.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Gets the specified component from the GameObject if it exists; otherwise, adds the component and returns it.
        /// </summary>
        /// <typeparam name="T">The type of component to get or add.</typeparam>
        /// <param name="gameObject">The GameObject to get or add the component to.</param>
        /// <returns>The existing or newly added component of type T.</returns>
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (!component)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }

        /// <summary>
        /// Returns the object if it exists; otherwise, returns null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <returns>The object if it exists; otherwise, null.</returns>
        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;

        /// <summary>
        /// Destroys all child objects of the specified GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject whose child objects are to be destroyed.</param>
        public static void DestroyChildren(this GameObject gameObject) => gameObject.transform.DestroyChildren();
    }
}