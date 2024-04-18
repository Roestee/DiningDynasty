using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Structure.Pool_Spawner.Spawner
{
    public abstract class SpawnerBase<T> : MonoBehaviour where T: Object
    {
        private SpawnPoint[] _spawnPoints;
        private int _spawnPointIndex;
        
        protected abstract T GetObject();
        
        protected abstract void InternalStart();
        protected abstract void InternalAwake();
        protected abstract void OnSpawnDone(T spawnObject);

        protected SpawnPoint CurrentSpawnPoint() => _spawnPoints[_spawnPointIndex];
        protected SpawnPoint[] AllSpawnPoints() => _spawnPoints;
        
        protected virtual void Awake()
        {
            SetSpawnPoints();
            InternalAwake();
        }
        
        protected virtual void Start()
        {
            InternalStart();
        }

        private void SetSpawnPoints()
        {
            _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        }

        protected void SpawnLoop(int count, float delay)
        {
            if(delay != 0)
                StartCoroutine(SpawnEnumerator(count, delay));
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Spawn();
                }
            }
        }
        
        private IEnumerator SpawnEnumerator(int count, float delay)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn();
                yield return new WaitForSeconds(delay);
            }
        }
        
        private void IncreaseIndex()
        {
            _spawnPointIndex++;
            if (_spawnPointIndex >= _spawnPoints.Length)
            {
                _spawnPointIndex = 0;
            }
        }

        protected abstract GameObject AdjustValues(T spawnObject);

        public T Spawn(Action<T> onSpawnDone = null)
        {
            if(_spawnPoints.Length == 0)
                SetSpawnPoints();

            var spawnedObject = GetObject();
            var gameObjectOfSpawn = AdjustValues(spawnedObject);
            var spawnPoint = _spawnPoints[_spawnPointIndex];
            
            spawnPoint.spawnObject = gameObjectOfSpawn;
            gameObjectOfSpawn.transform.position = _spawnPoints[_spawnPointIndex].transform.position;
            
            onSpawnDone?.Invoke(spawnedObject);
            OnSpawnDone(spawnedObject);
            IncreaseIndex();
            return spawnedObject;
        }
    }
}