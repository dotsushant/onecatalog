using System;
using System.Collections.Generic;

namespace OneCatalog.Helpers
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> predicate)
        {
            foreach (var item in collection) predicate(item);
        }
    }
}