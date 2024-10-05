using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Provides extension methods for the <see cref="System"/> namespace.
    /// </summary>
    public static class SystemExtensions
    {
        /// <summary>
        /// Returns a value indicating the sign of a floating-point number.
        /// </summary>
        /// <param name="value">The floating-point number whose sign is to be determined.</param>
        /// <returns>
        /// A float indicating the sign of <paramref name="value"/>:
        /// <list type="bullet">
        /// <item>
        /// <description>1 if <paramref name="value"/> is greater than zero.</description>
        /// </item>
        /// <item>
        /// <description>-1 if <paramref name="value"/> is less than zero.</description>
        /// </item>
        /// <item>
        /// <description>0 if <paramref name="value"/> is equal to zero.</description>
        /// </item>
        /// </list>
        /// </returns>
        public static float FloatSign(this float value)
        {
            return value > 0 ? 1f : (value < 0 ? -1f : 0);
        }

        /// <summary>
        /// Returns a random element from the list.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the list.</typeparam>
        /// <param name="list">The list from which to retrieve a random element.</param>
        /// <returns>A randomly selected element from the list.</returns>
        /// <exception cref="ArgumentException">Thrown when the list is either null or empty.</exception>
        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) throw new ArgumentException("List.GetRandomElement() : List is empty or null", nameof(list));
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}