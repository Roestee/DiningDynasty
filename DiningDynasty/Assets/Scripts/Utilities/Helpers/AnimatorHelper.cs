using UnityEngine;

namespace Utilities.Helpers
{
    public class AnimatorHelper
    {
        public static float GetAnimLenght(Animator animator, string animName)
        {
            AnimationClip first = null;
            foreach (var ac in animator.runtimeAnimatorController.animationClips)
            {
                if (ac.name == animName)
                {
                    first = ac;
                    break;
                }
            }
            return first.length;  
        }
    }
}