using Project.MeshOpener;
using Sirenix.Utilities;
using Structure.Player;
using Structure.Player.Stack;
using UnityEngine;

namespace Project.Fields.TomatoField
{
    public class TomatoTree : MonoBehaviour, IOpenerMesh
    {
        private Tomato[] _tomatoes;
        private PlayerInteractable _playerInteractable;

        private void Start()
        {
            _playerInteractable = GetComponentInChildren<PlayerInteractable>(true);
            _playerInteractable.OnPlayerInteract += OnPlayerInteract;
            
            _tomatoes = GetComponentsInChildren<Tomato>(true);
            _tomatoes.ForEach(p => p.Init());
        }
        public void OnMeshOpen()
        {
            _tomatoes.ForEach(p => StartCoroutine(p.Grow()));
        }

        private void OnPlayerInteract(PlayerBase player, bool interact)
        {
            
        }

        private void OnDestroy()
        {
            _playerInteractable.OnPlayerInteract -= OnPlayerInteract;
        }
    }
}