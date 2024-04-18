using UnityEngine;

namespace Structure.Pool_Spawner.Spawner.SpawnerWithOutPool
{
    public abstract class SpawnerGameObject : SpawnerBase<GameObject>
    {
        protected override GameObject AdjustValues(GameObject spawnObject)
        {
            return spawnObject;
        }
    }
}