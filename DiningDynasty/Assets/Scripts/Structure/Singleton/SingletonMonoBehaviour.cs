using UnityEngine;

namespace Structure.Singleton
{
    /// <summary>
    /// Singleton class
    /// </summary>
    /// <typeparam name="T">Type of the SingletonMonoBehaviour</typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        /// <summary>
        /// The static reference to the instance
        /// </summary>
        public static T Instance { get; protected set; }

        protected virtual void InstanceSet()
        {
        }

        /// <summary>
        /// Awake method to associate singleton with instance
        /// </summary>
        private static object _lockObject = new object();

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                lock (_lockObject)
                {
                    if (Instance == null)
                    {
                        Instance = (T) this;
                        InstanceSet();
                    }
                }
            }
            else
            {
                Destroy(this);
                Debug.LogError("Singleton object already defined: " + gameObject.name);
            }
        }

        /// <summary>
        /// OnDestroy method to clear singleton association
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}