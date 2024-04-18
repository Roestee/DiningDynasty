using Structure.Pool_Spawner.Interfaces;
using Structure.Pool_Spawner.Pools;
using UnityEngine;

namespace Structure.Pool_Spawner.Spawner.SpawnerWithPool
{
    public abstract class SpawnerMonoWithPoolSimple<T> : SpawnerMonoBase<T> where T: MonoBehaviour, IPoolMemberSimple
    {
        protected MonoBehaviorPool<T> MonoBehaviorPool;

        protected sealed override void Awake()
        {
            base.Awake();
            //
        }
        
        protected sealed override void Start()
        {
            base.Start();
            MonoBehaviorPool = PoolsManager.Instance.GetMyPoolSimple<T>();
        }

        protected override T GetObject()
        {
            return MonoBehaviorPool.Pull();
        }
    }
}