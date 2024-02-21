using UnityEngine;

namespace Structure.Player
{
    public enum PlayerMovementType
    {
        Idle,
        Runner
    }
    
    public abstract class PlayerMovementBase : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed;

        public bool CanMove { get; set; } = true;

        public abstract void Move(Vector3 input, float delta);
    }
}