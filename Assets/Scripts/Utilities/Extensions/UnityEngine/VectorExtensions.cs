namespace UnityEngine
{
    /// <summary>
    /// Provides extension methods for the Vector2, Vector3, and Vector4 structs to modify and add values to vectors.
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns a new Vector2 with the specified components replaced.
        /// </summary>
        /// <param name="vector">The original Vector2.</param>
        /// <param name="x">The new x component, or null to keep the original x component.</param>
        /// <param name="y">The new y component, or null to keep the original y component.</param>
        /// <returns>A new Vector2 with the specified components replaced.</returns>
        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(x ?? vector.x, y ?? vector.y);
        }

        /// <summary>
        /// Returns a new Vector2 with the specified components added.
        /// </summary>
        /// <param name="vector">The original Vector2.</param>
        /// <param name="x">The x component to add.</param>
        /// <param name="y">The y component to add.</param>
        /// <returns>A new Vector2 with the specified components added.</returns>
        public static Vector2 Add(this Vector2 vector, float x = 0, float y = 0)
        {
            return new Vector2(x: vector.x + x, y: vector.y + y);
        }

        /// <summary>
        /// Returns a new Vector3 with the specified components replaced.
        /// </summary>
        /// <param name="vector">The original Vector3.</param>
        /// <param name="x">The new x component, or null to keep the original x component.</param>
        /// <param name="y">The new y component, or null to keep the original y component.</param>
        /// <param name="z">The new z component, or null to keep the original z component.</param>
        /// <returns>A new Vector3 with the specified components replaced.</returns>
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        /// <summary>
        /// Returns a new Vector3 with the specified components added.
        /// </summary>
        /// <param name="vector">The original Vector3.</param>
        /// <param name="x">The x component to add.</param>
        /// <param name="y">The y component to add.</param>
        /// <param name="z">The z component to add.</param>
        /// <returns>A new Vector3 with the specified components added.</returns>
        public static Vector3 Add(this Vector3 vector, float x = 0, float y = 0, float z = 0)
        {
            return new Vector3(x: vector.x + x, y: vector.y + y, z: vector.z + z);
        }

        /// <summary>
        /// Returns a new Vector4 with the specified components replaced.
        /// </summary>
        /// <param name="vector">The original Vector4.</param>
        /// <param name="x">The new x component, or null to keep the original x component.</param>
        /// <param name="y">The new y component, or null to keep the original y component.</param>
        /// <param name="z">The new z component, or null to keep the original z component.</param>
        /// <param name="w">The new w component, or null to keep the original w component.</param>
        /// <returns>A new Vector4 with the specified components replaced.</returns>
        public static Vector4 With(this Vector4 vector, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            return new Vector4(x ?? vector.x, y ?? vector.y, z ?? vector.z, w ?? vector.w);
        }

        /// <summary>
        /// Returns a new Vector4 with the specified components added.
        /// </summary>
        /// <param name="vector">The original Vector4.</param>
        /// <param name="x">The x component to add.</param>
        /// <param name="y">The y component to add.</param>
        /// <param name="z">The z component to add.</param>
        /// <param name="w">The w component to add.</param>
        /// <returns>A new Vector4 with the specified components added.</returns>
        public static Vector4 Add(this Vector4 vector, float x = 0, float y = 0, float z = 0, float w = 0)
        {
            return new Vector4(x: vector.x + x, y: vector.y + y, z: vector.z + z, w: vector.w + w);
        }
    }
}