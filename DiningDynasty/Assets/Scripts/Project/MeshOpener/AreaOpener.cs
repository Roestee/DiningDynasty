using Project.Areas;
using UnityEngine;

namespace Project.MeshOpener
{
    public class AreaOpener : MeshOpenerBase<AreaType>
    {
        [SerializeField] private AreaType areaType;

        private AreaBase _area;
        
        protected override AreaType GetSpecialType() =>  areaType;
        
        public override void OnMeshOpen()
        {
            _area = GetComponent<AreaBase>();
            _area.IsUnlock = true;
        }
    }
}