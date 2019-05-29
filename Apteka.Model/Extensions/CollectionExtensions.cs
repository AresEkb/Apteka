using System.Collections.Generic;

namespace Apteka.Model.Extensions
{
    public static class CollectionExtensions
    {
        // https://stackoverflow.com/a/26360010/632199
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            if (destination is List<T> list)
            {
                list.AddRange(source);
            }
            else
            {
                foreach (T item in source)
                {
                    destination.Add(item);
                }
            }
        }
    }
}
