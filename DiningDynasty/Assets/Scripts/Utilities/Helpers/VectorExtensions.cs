using UnityEngine;

namespace Utilities.Helpers
{
    public static class VectorExtensions
    {
        public static Vector2 ToV2(this Vector3 input) => new Vector2(input.x, input.y);

        public static Vector3 Flat(this Vector3 input) => new Vector3(input.x, 0, input.z);

        public static Vector3Int ToVector3Int(this Vector3 vec3) => new Vector3Int((int)vec3.x, (int)vec3.y, (int)vec3.z);
    }
}