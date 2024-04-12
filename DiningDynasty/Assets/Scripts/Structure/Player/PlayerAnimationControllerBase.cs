using UnityEngine;
using Utilities.Helpers;

namespace Structure.Player
{
    public abstract class PlayerAnimationControllerBase : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Sit = Animator.StringToHash("Sit");
        
        protected Animator Anim;

        public void SetMovementSpeed(float speed = 1) => Anim.SetFloat(Speed, speed);       
        public float GetSitDuration() => AnimatorHelper.GetAnimLenght(Anim, "SitDown");
        
        protected virtual void Awake()
        {
            Anim = GetComponentInChildren<Animator>(true);
        }

        public void SitToChair()
        {
            Anim.SetTrigger(Sit);
        }
    }
}