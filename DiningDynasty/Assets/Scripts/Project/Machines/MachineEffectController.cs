using Sirenix.Utilities;
using UnityEngine;

namespace Project.Machines
{
    public class MachineEffectController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] cookingEffects;
        [SerializeField] private ParticleSystem[] cookedEffects;

        public void SetActiveCookingEffects(bool activate = true)
        {
            cookingEffects.ForEach(p =>
            {
                if (activate)
                    p.Play();
                else
                    p.Stop();
            });
        }
        
        public void PlayCookedEffect()
        {
            cookedEffects.ForEach(p => p.Play());
        }
    }
}