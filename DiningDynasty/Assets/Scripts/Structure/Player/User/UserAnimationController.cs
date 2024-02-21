using UnityEngine;

namespace Structure.Player.User
{
    public class UserAnimationController : PlayerAnimationControllerBase
    {
        private Animator _anim;
        
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>(true);
        }

        public void SetMovementSpeed(float inputVertical)
        {
            _anim.SetFloat(Speed, inputVertical);
        }
    }
}