using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamBlog.Utils
{
    public static class ObjectExtensions
    {
        public static T[] AsArray<T>(this T obj)
        {
            return new T[] {obj};
        }
    }

    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items.EmptyIfNull())
            {
                action(item);
            }
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> items)
        {
            return items ?? Enumerable.Empty<T>();
        }
    }
}