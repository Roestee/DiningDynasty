using System;
using System.Linq;
using Project.Coins;
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
        
        public Pool<PooledPlayerStack> Pool { get; set; }
    }
    
    public class PoolsManager : SingletonMonoBehaviour<PoolsManager>
    {
        [SerializeField] private GameObject coinPrefab;

        [Header("Player Stacks")] 
        [SerializeField] private StackPoolClass[] playerStackPools;

        public Pool<Coin> CoinPool { get; private set; }
        
        private void Start()
        {
            CoinPool = new Pool<Coin>(new PrefabFactory<Coin>(coinPrefab, transform));

            foreach (var pool in playerStackPools)
                pool.Pool = new Pool<PooledPlayerStack>(new PrefabFactory<PooledPlayerStack>(pool.poolObject, transform));
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