using System.Collections;
using System.Linq;
using Project.MeshOpener;
using Sirenix.Utilities;
using Structure.GenericObjectPooling;
using Structure.Player;
using Structure.Player.Stack;
using UnityEngine;
using Utilities.Helpers;

namespace Project.Fields.TomatoField
{
    public class TomatoTree : MonoBehaviour, IOpenerMesh<FieldType>
    {
        [SerializeField] private float waitBeforeTakeStack = 0.5f;
        
        private Tomato[] _tomatoes;
        private PlayerInteractable _playerInteractable;

        private PlayerBase _currentInteract;

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
            if (!interact)
            {
                _currentInteract = null;
                return;
            }

            if (_currentInteract != null)
                return;

            _currentInteract = player;
            StartCoroutine(TakeStack());
        }

        private IEnumerator TakeStack()
        {
            while (_currentInteract != null)
            {
                yield return GeneralHelpers.GetWait(waitBeforeTakeStack);

                if (_currentInteract == null)
                    yield break;
                
                if (!_currentInteract.PlayerStackController.CanTakeStack())
                    continue;
                
                if(!_tomatoes.Any(p=>p.IsGrown))
                    continue;

                var tomato = _tomatoes.FirstOrDefault(p => p.IsGrown);
                StartCoroutine(tomato.Grow());
                
                var stack = PoolsManager.Instance.GetStackPool(PlayerStackType.TomatoStack).Pull();
                stack.transform.SetPositionAndRotation(tomato.transform.position, tomato.transform.rotation);
                stack.OnPlayerInteract(_currentInteract, true);
            }
        }

        private void OnDestroy()
        {
            _playerInteractable.OnPlayerInteract -= OnPlayerInteract;
        }
    }
}