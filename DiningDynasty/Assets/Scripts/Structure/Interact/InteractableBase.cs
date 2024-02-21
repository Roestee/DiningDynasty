using UnityEngine;
using Utilities.Helpers;

namespace Structure.Interact
{
    public enum ComponentCheck
    {
        CurrentObject,
        ParentObjects,
        ChildObjects
    }
    
    public abstract class InteractableBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        protected virtual ComponentCheck[] ComponentChecks() => new[]
            { ComponentCheck.CurrentObject, ComponentCheck.ParentObjects, ComponentCheck.ChildObjects };
        
        protected abstract void OnTriggerInteract(T actor);
        protected abstract void OnTriggerInteractExit(T actor);
        
        protected abstract void OnTriggeredEnterVirtual(T actor, Collider other);
        protected abstract void OnTriggerExitVirtual(T actor, Collider other);

        protected virtual bool PreConditionOnTrigger() {return true;}
        protected virtual bool PreConditionOnTrigger(Collider other) {return true;}
        protected virtual bool AfterConditionOnTrigger(T actor) {return true;}

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!PreConditionOnTrigger())
                return;
            
            if (!PreConditionOnTrigger(other))
                return;

            T objectToCheck = null;
            foreach (var componentCheck in ComponentChecks())
            {
                if (CheckComponent(ref objectToCheck, other, componentCheck))
                    break;
            }

            if (!other.gameObject.layer.LayerMaskLayerCompare(layerMask))
                return;
            
            if (!AfterConditionOnTrigger(objectToCheck))
                return;
            
            OnTriggeredEnterVirtual(objectToCheck, other);
        }
        
        protected virtual void OnTriggerExit(Collider other)
        {
            T objectToCheck = null;
            foreach (var componentCheck in ComponentChecks())
            {
                if (CheckComponent(ref objectToCheck, other, componentCheck))
                    break;
            }

            if (!other.gameObject.layer.LayerMaskLayerCompare(layerMask))
                return;
            
            OnTriggerExitVirtual(objectToCheck, other);
        }

        private bool CheckComponent(ref T objectToCheck, Collider other, ComponentCheck componentCheck)
        {
            if (componentCheck == ComponentCheck.CurrentObject)
                objectToCheck = other.GetComponent<T>();
            if (componentCheck == ComponentCheck.ParentObjects)
                objectToCheck = other.GetComponentInParent<T>();
            if (componentCheck == ComponentCheck.ChildObjects)
                objectToCheck = other.GetComponentInChildren<T>();

            return objectToCheck != null;
        }
    }
}