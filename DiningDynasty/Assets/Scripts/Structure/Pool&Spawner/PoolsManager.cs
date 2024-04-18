using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Structure.Pool_Spawner.Interfaces;
using Structure.Pool_Spawner.Pools;
using Structure.Singleton;
using UnityEditor;
using UnityEngine;
using Object = System.Object;
using Type = System.Type;

namespace Structure.Pool_Spawner
{
    public abstract class PoolObjects
    {
    }
    
    [Serializable]
    public class SimplePoolObjects : PoolObjects
    {
        public int initSpawnCount;
        public GameObject gameObject;
        
        public SimplePoolObjects(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    [Serializable]
    public class TypedPoolObjects
    {
        public string typeName;
        public List<SimplePoolObjects> typedObjects;

        public TypedPoolObjects(string typeName, SimplePoolObjects typedObject)
        {
            this.typeName = typeName;
            typedObjects = new List<SimplePoolObjects> { typedObject };
        }
    }

    public struct TypedPoolsDictionary<T, TLogic> where T: MonoBehaviour, IPoolMemberWithType<TLogic>
    {
        private Dictionary<TLogic, MonoBehaviorPool<T>> _dictionary;
        public Dictionary<TLogic, MonoBehaviorPool<T>> GetAllTypedPools() => _dictionary;

        //Don't change the name, if you change pools manager will give you error
        public void AddToDictionary(TLogic tLogic, MonoBehaviorPool<T> pool)
        {
            _dictionary ??= new Dictionary<TLogic, MonoBehaviorPool<T>>();
            _dictionary.Add(tLogic, pool);
        }
    }
    
    public class PoolsManager : SingletonMonoBehaviour<PoolsManager>
    {
        [SerializeField] private List<SimplePoolObjects> simplePoolObjects;
        [Space] 
        [SerializeField] private List<TypedPoolObjects> typedPoolObjects;

        private Dictionary<Type, Object> _monoSimplePoolDictionary; //Object is MonoBehaviorPoolSimple
        private Dictionary<Type, Object> _monoTypedPoolDictionary; //Object is TypedPoolsDictionary

        protected override void Awake()
        {
            base.Awake();
            MonoPoolsInitiate();
            MonoPoolTypeInvolvedInitiate();
            simplePoolObjects = null;
            typedPoolObjects = null;
            GC.Collect();
        }

        [Button("Fetch All Pool Objects")]
        private void FetchAllPoolObjects()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs" });
            foreach (string guid in guids)
            {
                string myObjectPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(myObjectPath);
                
                var simplePoolComponent = GetMonoWithInterface(go.GetComponents<MonoBehaviour>(), "IPoolMemberSimple");
                var typedPoolComponent = GetMonoWithInterface(go.GetComponents<MonoBehaviour>(), "IPoolMemberWithType`1");

                if (simplePoolComponent != null && simplePoolObjects.FirstOrDefault(sp => sp.gameObject == go) == null)
                    simplePoolObjects.Add(new SimplePoolObjects(go));
                if (typedPoolComponent != null)
                {
                    var getTypedPoolObject = typedPoolObjects.FirstOrDefault(tp => tp.typeName == typedPoolComponent.GetType().Name);
                    if (getTypedPoolObject == null)
                        typedPoolObjects.Add(new TypedPoolObjects(typedPoolComponent.GetType().Name, new SimplePoolObjects(go)));
                    else
                    {
                        var typedObject = getTypedPoolObject.typedObjects.FirstOrDefault(t => t.gameObject == go);
                        if (typedObject == null)
                            getTypedPoolObject.typedObjects.Add(new SimplePoolObjects(go));
                    }
                }
            }
        }
        
        private void MonoPoolsInitiate()
        {
            Type openType = typeof(MonoBehaviorPool<>);
            _monoSimplePoolDictionary = new Dictionary<Type, Object>(simplePoolObjects.Count);

            foreach (var simplePoolObject in simplePoolObjects)
            {
                var component = GetMonoWithInterface(simplePoolObject.gameObject.GetComponents<MonoBehaviour>(), "IPoolMemberSimple");
                if (component == null)
                {
                    Debug.LogError("There is no IPoolMember inherited from this element");
                    return;
                }
                
                var componentType = component.GetType();
                Type closedType = openType.MakeGenericType(componentType);
                var objectParent = new GameObject($"{component.name} Pool") {transform = {parent = transform}};
                var monoPool = Activator.CreateInstance(closedType,
                    component,
                    objectParent.transform,
                    simplePoolObject.initSpawnCount);
                
                _monoSimplePoolDictionary[componentType] = monoPool;
            }
        }

        private void MonoPoolTypeInvolvedInitiate()
        {
            Type poolOpenType = typeof(MonoBehaviorPool<>);
            _monoTypedPoolDictionary = new Dictionary<Type, Object>(typedPoolObjects.Count);

            foreach (var typedPoolObject in typedPoolObjects)
            {
                var componentCheck = GetMonoWithInterface(typedPoolObject.typedObjects[0].gameObject.GetComponents<MonoBehaviour>(), "IPoolMemberWithType`1");
                if (componentCheck == null)
                {
                    Debug.LogError("There is no IPoolMember inherited from this element", componentCheck);
                    return;
                }

                var componentType = componentCheck.GetType();
                Type poolClosedType = poolOpenType.MakeGenericType(componentType);
                Type interfaceType = componentType.GetInterface("IPoolMemberWithType`1");
                Type typedPoolDictionaryCloseType = typeof(TypedPoolsDictionary<,>).MakeGenericType(componentType, interfaceType.GenericTypeArguments[0]);
                var getTypeForPoolMethod = interfaceType.GetMethod("GetTypeForPool");
                var addToDictionaryMethod = typedPoolDictionaryCloseType.GetMethod("AddToDictionary");
                
                var typedPoolDictionary = Activator.CreateInstance(typedPoolDictionaryCloseType);
                _monoTypedPoolDictionary[componentCheck.GetType()] = typedPoolDictionary;
                
                foreach (var typedObject in typedPoolObject.typedObjects)
                {
                    var component = GetMonoWithInterface(typedObject.gameObject.GetComponents<MonoBehaviour>(), "IPoolMemberWithType`1");
                    var valueOfGenericTypeOfInterface = getTypeForPoolMethod.Invoke(component, null);
                    var objectParent = new GameObject($"{componentType.Name} : {valueOfGenericTypeOfInterface} Pool") {transform = {parent = transform}};
                    var monoPool = Activator.CreateInstance(poolClosedType,
                        component,
                        objectParent.transform,
                        typedObject.initSpawnCount);
                    
                    addToDictionaryMethod.Invoke(typedPoolDictionary, new[] { valueOfGenericTypeOfInterface, monoPool });
                }
            }
        }

        private MonoBehaviour GetMonoWithInterface(MonoBehaviour[] components, string interfaceName)
        {
            foreach (var component in components)
            {
                if (component == null)
                    throw new NullReferenceException("There is prefab has contains missing script");
                var componentType = component.GetType();
                var interfaceType = componentType.GetInterface(interfaceName);
                if (interfaceType != null)
                    return component;
            }
            return null;
        }

        public MonoBehaviorPool<T> GetMyPoolSimple<T>() where T : MonoBehaviour, IPoolMemberSimple
        {
            var pool = (MonoBehaviorPool<T>) _monoSimplePoolDictionary[typeof(T)];
            if(pool == null)
                Debug.LogError("Pool Not Exist");
            
            return pool;
        }
        
        public Dictionary<TLogic, MonoBehaviorPool<T>> GetMyPoolsOfTyped<T, TLogic>() where T : MonoBehaviour, IPoolMemberWithType<TLogic>
        {
            var typedPoolsDictionary = (TypedPoolsDictionary<T, TLogic>)_monoTypedPoolDictionary[typeof(T)];
            return typedPoolsDictionary.GetAllTypedPools();
        }
        
        public MonoBehaviorPool<T> GetMyPoolTyped<T, TLogic>(TLogic tLogic) where T : MonoBehaviour, IPoolMemberWithType<TLogic>
        {
            var typedPoolsDictionary = GetMyPoolsOfTyped<T, TLogic>();
            return typedPoolsDictionary[tLogic];
        }
    }
}