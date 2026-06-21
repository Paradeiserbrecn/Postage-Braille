using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Provides utility helper methods used throughout the application.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Randomly shuffles the elements of a list in place using
        /// the Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <param name="list">The list to shuffle.</param>
        public static void ShuffleList(List<string> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                var randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}