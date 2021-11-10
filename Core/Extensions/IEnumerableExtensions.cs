using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ThrowIfEmpty<T>(this IEnumerable<T> source, string name)
        {
            if (source == null || !source.Any())
                throw new ArgumentNullException(name);
        }
    }
}
