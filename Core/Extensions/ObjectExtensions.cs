using System;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        public static T[] AsArray<T>(this T value)
            => value != null ?
                new T[] { value } :
                Array.Empty<T>();
    }
}
