using UnityEngine;

namespace Project.MeshOpener
{
    public class FieldOpener : MeshOpenerBase<FieldType>
    {
        [SerializeField] private FieldType fieldType;
        
        protected override FieldType GetSpecialType() => fieldType;
        
        public override void OnMeshOpen()
        {
            
        }
    }
}