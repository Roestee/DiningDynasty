using Structure.GenericObjectPooling.Abstracts;
using UnityEngine;

namespace Structure.GenericObjectPooling
{
    public class PrefabFactory<T> : IFactory<T> where T: MonoBehaviour
    {
        private GameObject _prefab;
        private Transform _parent;
        private string _name;
        private int _index;

        public PrefabFactory(GameObject prefab) : this(prefab, null, prefab.name) { }
        public PrefabFactory(GameObject prefab, Transform parent) : this(prefab, parent, prefab.name) { }

        public PrefabFactory(GameObject prefab, Transform parent, string name)
        {
            _prefab = prefab;
            _parent = parent;
            _name = name;
        }

        public T Create()
        {
            var tempGameObject = Object.Instantiate(_prefab, _parent, true) as GameObject;
            tempGameObject.name = _name + _index;
            tempGameObject.SetActive(false);
            var objectOfType = tempGameObject.GetComponent<T>();
            _index++;
            return objectOfType;
        }
    }
}