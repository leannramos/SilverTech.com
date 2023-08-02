using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telerik.Sitefinity
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Shuffle the Enumerable around in a random order
        /// ** Sitefinitysteve.com Extension, from StackOverflow **
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}