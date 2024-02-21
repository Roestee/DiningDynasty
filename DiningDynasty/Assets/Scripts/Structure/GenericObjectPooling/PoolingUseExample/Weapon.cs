using System;
using UnityEngine;

namespace Structure.GenericObjectPooling.PoolingUseExample
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletParent;
        
        private Pool<Bullet> _pool;

        private void Start()
        {
            _pool = new Pool<Bullet>(new PrefabFactory<Bullet>(bulletPrefab, bulletParent), 4);
        }
        
        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.S)) 
            {
                Fire();
            }
        }

        private void Fire()
        {
            var bullet = _pool.Pull();
            EventHandler handler = null;
            handler = (sender, e) =>
            {
                _pool.Push(bullet);
                bullet.OnDeath -= handler;
            };
                        
            bullet.OnDeath += handler;
            bullet.gameObject.SetActive(true);
        }
    }
}