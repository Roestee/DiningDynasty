using System;
using DG.Tweening;
using Structure.GenericObjectPooling.Abstracts;
using UnityEngine;

namespace Project.Coins
{
    public class Coin : MonoBehaviour, IThrowable, IPoolMember
    {
        public event Action<IPoolMember> OnDeath;
        
        [SerializeField] private float jumpPower = 5f;
        [SerializeField] private float jumpDuration = 0.8f;
        
        public void Throw(Transform targetTf)
        {
            transform.DOJump(targetTf.position, jumpPower, 1, jumpDuration).
                OnComplete(()=> OnDeath?.Invoke(this));
        }

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

        #endregion
    }
}