using System.Text.Json;


namespace Toodle.CommonExtensions.Cache
{
    /// <summary>
    /// Provides methods for generating cache keys.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Generates a fast cache key using object's GetHashCode. Note: Hash codes are not guaranteed to be consistent across application restarts.
        /// </summary>
        /// <typeparam name="T">The type of object to generate a key for.</typeparam>
        /// <param name="obj">The object to generate a key for.</param>
        /// <param name="prefix">Optional prefix for the cache key.</param>
        /// <returns>A cache key string in the format [prefix_]TypeName_Hash.</returns>
        /// <example>
        /// <code>
        /// var person = new Person { Id = 1, Name = "John" };
        /// string key = person.ToCacheKeyFast(); // Returns "Person_[hash]"
        /// string keyWithPrefix = person.ToCacheKeyFast("App1"); // Returns "App1_Person_[hash]"
        /// </code>
        /// </example>
        public static string ToCacheKeyFast<T>(this T obj, string? prefix = null) where T : notnull
        {
            var hash = JsonSerializer.Serialize(obj).GetHashCode();

            return string.IsNullOrEmpty(prefix)
                ? $"{typeof(T).Name}_{hash:X8}"
                : $"{prefix}_{typeof(T).Name}_{hash:X8}";
        }

        /// <summary>
        /// Generates a stable cache key using FNV-1a hash. Keys will be consistent across application restarts.
        /// </summary>
        /// <typeparam name="T">The type of object to generate a key for.</typeparam>
        /// <param name="obj">The object to generate a key for.</param>
        /// <param name="prefix">Optional prefix for the cache key.</param>
        /// <returns>A cache key string in the format [prefix_]TypeName_Hash.</returns>
        /// <example>
        /// <code>
        /// var person = new Person { Id = 1, Name = "John" };
        /// string key = person.ToCacheKeyStable(); // Returns "Person_[hash]"
        /// string keyWithPrefix = person.ToCacheKeyStable("App1"); // Returns "App1_Person_[hash]"
        /// </code>
        /// </example>
        public static string ToCacheKeyStable<T>(this T obj, string? prefix = null) where T : notnull
        {
            var json = JsonSerializer.Serialize(obj);

            // FNV-1a 32-bit hash
            const uint FNV_PRIME = 16777619;
            const uint FNV_OFFSET_BASIS = 2166136261;

            uint hash = FNV_OFFSET_BASIS;
            foreach (var c in json)
            {
                hash ^= c;
                hash *= FNV_PRIME;
            }

            return string.IsNullOrEmpty(prefix)
                ? $"{typeof(T).Name}_{hash:X8}"
                : $"{prefix}_{typeof(T).Name}_{hash:X8}";
        }
    }
}
