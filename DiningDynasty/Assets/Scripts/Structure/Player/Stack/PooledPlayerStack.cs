using System;
using Structure.GenericObjectPooling.Abstracts;

namespace Structure.Player.Stack
{
    public class PooledPlayerStack : PlayerStack, IPoolMember
    {
        public event Action<IPoolMember> OnDeath;

        #region Pool

        public void OnCreate()
        {
            
        }

        public void OnEnterPool()
        {
            gameObject.SetActive(false);
        }

        public void OnExitPool()
        {
            gameObject.SetActive(true);
        }
        
        public void PushToPool()
        {
            OnDeath?.Invoke(this);
        }

        #endregion
    }
}