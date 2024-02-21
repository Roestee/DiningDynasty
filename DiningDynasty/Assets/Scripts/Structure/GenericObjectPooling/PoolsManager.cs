using Project.MeshOpener;
using Structure.Singleton;
using UnityEngine;

namespace Structure.GenericObjectPooling
{
    public class PoolsManager : SingletonMonoBehaviour<PoolsManager>
    {
        [SerializeField] private GameObject coinPrefab;

        public Pool<Coin> CoinPool { get; private set; }

        private void Start()
        {
            CoinPool = new Pool<Coin>(new PrefabFactory<Coin>(coinPrefab, transform));
        }
    }
}