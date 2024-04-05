using Structure.MeshTypes;

namespace Project.Player
{
    public class PlayerHatController : GOMeshTypeController<Hat, int>
    {
        private void Start()
        {
            ChooseRandomMesh();
        }

        public override void SetMeshAttribute()
        {
            
        }
    }
}