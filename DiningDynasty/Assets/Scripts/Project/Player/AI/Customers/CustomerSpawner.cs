using Structure.GenericObjectPooling;
using Structure.GenericObjectPooling.Abstracts;
using UnityEngine;

namespace Project.Player.AI.Customers
{
    public class CustomerSpawner : MonoBehaviour
    {
        private Pool<Customer> _pool;
        private Transform _spawnPoint;

        private void Start()
        {
            _pool = PoolsManager.Instance.CustomerPool;
        }

        private void SpawnCustomer()
        {
            var customer = _pool.Pull();
            customer.transform.position = _spawnPoint.position;
            customer.OnDeath += PushToPool;
            customer.StateMachine.SetDefaultState();
        }

        private void PushToPool(IPoolMember poolMember)
        {
            var customer = (Customer)poolMember;
            customer.OnDeath -= PushToPool;
            _pool.Push(customer);
        }
    }
}