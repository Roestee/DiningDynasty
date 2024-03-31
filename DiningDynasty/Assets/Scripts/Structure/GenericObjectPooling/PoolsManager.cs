using Project.Coins;
using Structure.Player.Stack;
using Structure.Singleton;
using UnityEngine;

namespace Structure.GenericObjectPooling
{
    public class PoolsManager : SingletonMonoBehaviour<PoolsManager>
    {
        [SerializeField] private GameObject coinPrefab;

        [Header("Player Stacks")] 
        [SerializeField] private GameObject tomatoPrefab;

        public Pool<Coin> CoinPool { get; private set; }
        public Pool<PooledPlayerStack> TomatoPool { get; private set; }

        private void Start()
        {
            CoinPool = new Pool<Coin>(new PrefabFactory<Coin>(coinPrefab, transform));
            TomatoPool = new Pool<PooledPlayerStack>(new PrefabFactory<PooledPlayerStack>(tomatoPrefab, transform));
        }
    }
}