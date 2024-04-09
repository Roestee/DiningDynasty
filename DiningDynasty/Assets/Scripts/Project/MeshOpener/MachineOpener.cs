using UnityEngine;

namespace Project.MeshOpener
{
    public class MachineOpener : MeshOpenerBase<MachineType>
    {
        [SerializeField] private MachineType machineType;

        protected override MachineType GetSpecialType() => machineType;
        
        public override void OnMeshOpen()
        {
            
        }
    }
}