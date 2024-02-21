using System;
using Structure.GenericObjectPooling.Abstracts;
using UnityEngine;

namespace Structure.GenericObjectPooling.PoolingUseExample
{
    public class Bullet : MonoBehaviour, IPoolMember
    {
        public event Action<IPoolMember> OnDeath;

        [SerializeField] private float speed = 10f;

        private void Death()
        {
            OnDeath?.Invoke(this);
        }

        private void Update()
        {
            transform.position += Vector3.forward * (speed * Time.deltaTime);
        }
        
        private void OnEnable()
        {
            Invoke(nameof(Death), 2f);
        }

        public void OnCreate()
        {
            
        }

        public void OnEnterPool()
        {
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void OnExitPool()
        {
            
        }
    }
}

