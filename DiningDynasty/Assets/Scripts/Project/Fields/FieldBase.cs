using System;
using Structure.Player;
using UnityEngine;

namespace Project.Fields
{
    public abstract class FieldBase : MonoBehaviour
    {
        public event Action<PlayerBase> OnCollect;
        
        protected void FireCollectEvent(PlayerBase player) => OnCollect?.Invoke(player);
        
        public abstract Vector3 GetAiCollectPoint();
        public abstract bool IsThereAvailableStack();
    }
}