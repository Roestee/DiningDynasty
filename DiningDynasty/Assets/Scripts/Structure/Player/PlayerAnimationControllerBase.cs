using UnityEngine;

namespace Structure.Player
{
    public abstract class PlayerAnimationControllerBase : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        protected Animator Anim;

        public void SetMovementSpeed(float speed = 1) => Anim.SetFloat(Speed, speed);
        
        protected virtual void Awake()
        {
            Anim = GetComponentInChildren<Animator>(true);
        }
    }
}