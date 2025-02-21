using System.Text.Json;

namespace Toodle.CommonExtensions.Json
{
    /// <summary>
    /// Provides extension methods for working with JSON serialization.
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Serializes an object to its JSON string representation.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        /// <example>
        /// <code>
        /// var person = new Person { Name = "John", Age = 30 };
        /// string json = person.ToJson(); // Returns {"Name":"John","Age":30}
        /// </code>
        /// </example>
        public static string ToJson<T>(this T obj) where T : notnull
        {
            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// Serializes an object to its JSON string representation using the specified options.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="options">Options to control the serialization behavior.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string ToJson<T>(this T obj, JsonSerializerOptions options) where T : notnull
        {
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
