using UnityEngine;

namespace Structure.Player.Stack
{
    public class StackCell : MonoBehaviour
    {
        public PlayerStack CurrentStack { get; set; }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, 0.2f * Vector3.one);
            Gizmos.color = Color.white;
        }
#endif

    }
}