using Project.MeshOpener;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Fields.TomatoField
{
    public class TomatoTree : MonoBehaviour, IOpenerMesh
    {
        private Tomato[] _tomatoes;

        private void Start()
        {
            _tomatoes = GetComponentsInChildren<Tomato>(true);
            _tomatoes.ForEach(p => p.Init());
        }

        public void OnMeshOpen()
        {
            _tomatoes.ForEach(p => StartCoroutine(p.Grow()));
        }
    }
}