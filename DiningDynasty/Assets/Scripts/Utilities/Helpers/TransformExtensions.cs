using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Helpers
{
    public static class TransformExtensions
    {
        public static List<Transform> GetAllChildren(this Transform obj)
        {
            var list = new List<Transform>();
            for (int i = 0; i < obj.childCount; i++)
            {
                list.Add(obj.GetChild(i));
            }

            return list;
        }
        
        public static void DeleteAllChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }
    }
}