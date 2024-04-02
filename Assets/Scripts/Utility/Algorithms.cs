using System;
using System.Collections.Generic;

namespace Utility
{
    public static class Algorithms
    {
        // STUB - Use seeded random, need to make a centralized random
        private static Random m_random = new System.Random();
        
        /// <summary>
        /// Shuffle the array using Fisher-Yattes shuffle.
        /// </summary>
        public static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + m_random.Next(n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }
        
        /// <summary>
        /// Shuffle the array using Fisher-Yattes shuffle.
        /// </summary>
        public static void Shuffle<T>(List<T> array)
        {
            int n = array.Count;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + m_random.Next(n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }
    }
}
