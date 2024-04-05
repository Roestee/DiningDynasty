using UnityEngine;

namespace Project.Fields
{
    public abstract class FieldBase : MonoBehaviour
    {
        public abstract Vector3 GetAiCollectPoint();
        public abstract bool IsThereAvailableStack();
    }
}