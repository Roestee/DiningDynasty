using System.Collections.Generic;
using Structure.Pool_Spawner.Interfaces;
using Structure.Pool_Spawner.Pools;
using UnityEngine;

namespace Structure.Pool_Spawner.Spawner.SpawnerWithPool
{
    public abstract class SpawnerMonoWithPoolWithType<T, TLogic> : SpawnerMonoBase<T> where T: MonoBehaviour, IPoolMemberWithType<TLogic>
    {
        private Dictionary<TLogic, MonoBehaviorPool<T>> _monoBehaviorPoolWithType;
        private TLogic _currentSpawnType;

        protected MonoBehaviorPool<T> GetPool() => _monoBehaviorPoolWithType[_currentSpawnType];
        
        protected sealed override void Awake()
        {
            base.Awake();
            //
        }

        protected sealed override void Start()
        {
            base.Start();
            _monoBehaviorPoolWithType = PoolsManager.Instance.GetMyPoolsOfTyped<T, TLogic>();
        }

        protected void SetSpawnType(TLogic type)
        {
            _currentSpawnType = type;
        }
        
        protected override T GetObject()
        {
            return _monoBehaviorPoolWithType[_currentSpawnType].Pull();
        }
    }
}