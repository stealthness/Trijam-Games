using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Core
{
    public class GameObjectUtils 
    {
        public static int[] ShuffleGameObjectsIndices<T>(T[] array)
        {
            List<int> indices = new List<int>(array.Length);
            for (int i = 0; i < array.Length; i++) indices.Add(i);

            for (int i = indices.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            return indices.ToArray();
        }
    }
}