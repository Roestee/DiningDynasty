using UnityEngine;

namespace Project.MeshOpener
{
    public class AreaOpener : MeshOpenerBase<AreaType>
    {
        [SerializeField] private AreaType areaType;
        
        protected override AreaType GetSpecialType() =>  areaType;
    }
}