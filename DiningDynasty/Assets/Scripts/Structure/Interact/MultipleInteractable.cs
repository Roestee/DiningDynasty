using System.Collections.Generic;
using UnityEngine;

namespace Structure.Interact
{
    public abstract class MultipleInteractable<T> : InteractableBase<T> where T : MonoBehaviour
    {
        protected List<T> Interacts = new List<T>();

        protected sealed override void OnTriggeredEnterVirtual(T actor, Collider other)
        {
            if (Interacts == null)
                Interacts = new List<T>();
            
            if (Interacts.Contains(actor))
                return;
            
            Interacts.Add(actor);
            OnTriggerInteract(actor);
        }

        protected sealed override void OnTriggerExitVirtual(T actor, Collider other)
        {
            OnTriggerInteractExit(actor);
            Interacts.Remove(actor);
        }
        
        protected void ClearInteracts()
        {
            Interacts.Clear();
        }
    }
}