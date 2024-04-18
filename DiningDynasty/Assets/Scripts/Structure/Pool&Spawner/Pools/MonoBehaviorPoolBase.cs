using Structure.Pool_Spawner.Interfaces;
using UnityEngine;

namespace Structure.Pool_Spawner.Pools
{
    public abstract class MonoBehaviorPoolBase<T> : PoolBase<T> where T: MonoBehaviour, IPoolMemberBase
    {
        private readonly Transform _objectParent;
        private int _objectCount;
        
        protected T SpawnPrefab;

        protected MonoBehaviorPoolBase(Transform objectParent)
        {
            _objectParent = objectParent;
        }

        private void EnterPool(T member)
        {
            member.gameObject.SetActive(false);
            member.transform.SetParent(_objectParent);
        }

        protected sealed override void OnEnterPool(T member)
        {
            member.OnEnterPool();
            EnterPool(member);
        }

        protected sealed override void OnExitPull(T member)
        {
            member.transform.SetParent(null);
            member.gameObject.SetActive(true);
            member.OnExitPool();
        }

        protected T CreateBase()
        {
            var monoObject = Object.Instantiate(SpawnPrefab);
            Object.DontDestroyOnLoad(monoObject.gameObject);
            monoObject.name += _objectCount++;
            monoObject.OnCreate();
            EnterPool(monoObject);
            return monoObject;
        }
    }
}