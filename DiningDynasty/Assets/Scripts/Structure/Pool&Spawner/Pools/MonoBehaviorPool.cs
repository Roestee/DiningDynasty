using Structure.Pool_Spawner.Interfaces;
using Structure.Pool_Spawner.PoolLogics;
using UnityEngine;

namespace Structure.Pool_Spawner.Pools
{
    public sealed class MonoBehaviorPool<T> : MonoBehaviorPoolBase<T> where T: MonoBehaviour, IPoolMemberBase
    {
        private readonly PoolLogicSimple<T> _poolLogicSimple;

        public MonoBehaviorPool(T spawnObject, Transform objectParent, int spawnCount) : base(objectParent)
        {
            SpawnPrefab = spawnObject;
            _poolLogicSimple = new PoolLogicSimple<T>(Create, OnEnterPool, OnExitPull);
            _poolLogicSimple.CreateMultiple(spawnCount);
        }
        
        public override T Pull()
        {
            var getObject = _poolLogicSimple.Pull();
            return getObject;
        }
        
        public override void Push(T member)
        {
            _poolLogicSimple.Push(member);
        }

        private T Create()
        {
            return CreateBase();
        }
    }
}