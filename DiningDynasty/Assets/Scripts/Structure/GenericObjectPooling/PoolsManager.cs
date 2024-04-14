using System;
using System.Linq;
using Project.Coins;
using Project.Player.AI.Customers;
using Structure.Player.Stack;
using Structure.Singleton;
using UnityEngine;

namespace Structure.GenericObjectPooling
{
    [Serializable]
    public class StackPoolClass
    {
        public PlayerStackType stackType;
        public GameObject poolObject;
        public int poolSize = 5;
        
        public Pool<PooledPlayerStack> Pool { get; set; }
    }
    
    public class PoolsManager : SingletonMonoBehaviour<PoolsManager>
    {
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject customerPrefab;

        [Header("Player Stacks")] 
        [SerializeField] private StackPoolClass[] playerStackPools;

        public Pool<Coin> CoinPool { get; private set; }
        public Pool<Customer> CustomerPool { get; private set; }
        
        private void Start()
        {
            CoinPool = new Pool<Coin>(new PrefabFactory<Coin>(customerPrefab, transform));
            CustomerPool = new Pool<Customer>(new PrefabFactory<Customer>(coinPrefab, transform));

            foreach (var pool in playerStackPools)
                pool.Pool = new Pool<PooledPlayerStack>(new PrefabFactory<PooledPlayerStack>(pool.poolObject, transform), pool.poolSize);
        }

        public Pool<PooledPlayerStack> GetStackPool(PlayerStackType stackType)
        {
            var pool = playerStackPools.FirstOrDefault(p => p.stackType == stackType);
            if (pool == null)
            {
                Debug.LogError($"{stackType} type pool doesn't exist!");
                return null;
            }

            return pool.Pool;
        }
    }
}