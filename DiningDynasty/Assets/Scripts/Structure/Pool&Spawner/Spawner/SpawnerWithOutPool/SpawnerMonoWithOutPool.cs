using UnityEngine;

namespace Structure.Pool_Spawner.Spawner.SpawnerWithOutPool
{
    public abstract class SpawnerMonoWithOutPool<T> : SpawnerMonoBase<T> where T: MonoBehaviour
    {
        protected T SpawnObject;

        protected override T GetObject()
        {
            return Instantiate(SpawnObject);
        }
    }
}