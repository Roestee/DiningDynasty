using Structure.MeshTypes;
using UnityEngine;

namespace Project.Player
{
    public class Hat : MonoBehaviour, IMeshType<int>
    {
        [SerializeField] private int id;
        
        public int GetSpecialType() => id;
        
        public void OnMeshSetActive(bool active = true)
        {
            gameObject.SetActive(active);
        }
    }
}