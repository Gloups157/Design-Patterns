using System;
using System.Collections.Generic;

namespace UnityEngine
{
    /// <summary>
    /// Provides extension methods for the Transform class to manipulate and iterate over child transforms.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Returns an enumerable collection of all children of the specified parent transform.
        /// </summary>
        /// <param name="parent">The Transform whose children are to be returned.</param>
        /// <returns>An IEnumerable&lt;Transform&gt; containing all the children of the parent transform.</returns>
        public static IEnumerable<Transform> Children(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                yield return child;
            }
        }

        /// <summary>
        /// Destroys all children of the specified parent transform.
        /// </summary>
        /// <param name="parent">The Transform whose children are to be destroyed.</param>
        public static void DestroyChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => Object.Destroy(child.gameObject));
        }

        /// <summary>
        /// Enables all children of the specified parent transform by setting their GameObject to active.
        /// </summary>
        /// <param name="parent">The Transform whose children are to be enabled.</param>
        public static void EnableChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(true));
        }

        /// <summary>
        /// Disables all children of the specified parent transform by setting their GameObject to inactive.
        /// </summary>
        /// <param name="parent">The Transform whose children are to be disabled.</param>
        public static void DisableChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(false));
        }

        /// <summary>
        /// Performs a specified action on all children of the parent transform.
        /// </summary>
        /// <param name="parent">The Transform whose children the action will be performed on.</param>
        /// <param name="action">An Action&lt;Transform&gt; delegate to execute on each child.</param>
        static void PerformActionOnChildren(this Transform parent, Action<Transform> action)
        {
            for (int i = 0; i < parent.childCount - 1; i++)
            {
                action(parent.GetChild(i));
            }
        }
    }
}