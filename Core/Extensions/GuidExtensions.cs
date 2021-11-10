using System;

namespace Core.Extensions
{
    public static class GuidExtensions
    {
        public static Guid EnsureNotEmpty(this Guid value, Guid @new)
            => value.IsEmpty() ? @new : value;

        public static bool IsEmpty(this Guid value)
            => value == Guid.Empty;

        public static void ThrowIfEmpty(this Guid value, string name)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException(name);
        }
    }
}
