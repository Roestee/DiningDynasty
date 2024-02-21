using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Helpers
{
    public static class ListExtensions
    {
        public static T Rand<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                var randomIndex = Random.Range(i, list.Count);
                SwapIndex(list, i, randomIndex);
            }
        }
        private static void SwapIndex<T>(IList<T> list, int current, int target)
        {
            (list[target], list[current]) = (list[current], list[target]);
        }
    }
}