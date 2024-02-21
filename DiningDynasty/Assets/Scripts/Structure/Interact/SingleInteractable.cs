using UnityEngine;

namespace Structure.Interact
{
    public abstract class SingleInteractable<T> : InteractableBase<T> where T : MonoBehaviour
    {
        protected T Interact;

        protected sealed override void OnTriggeredEnterVirtual(T actor, Collider other)
        {
            Interact = actor;
            OnTriggerInteract(Interact);
        }

        protected sealed override void OnTriggerExitVirtual(T actor, Collider other)
        {
            if (Interact != actor) 
                return;
            
            Interact = null;
            OnTriggerInteractExit(actor);
        }
    }
}