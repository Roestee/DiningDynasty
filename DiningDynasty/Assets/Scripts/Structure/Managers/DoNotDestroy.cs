using UnityEngine;

namespace Structure.Managers
{
    public class DoNotDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}