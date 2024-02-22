using System.Collections;
using Structure.Managers;
using Structure.Player;
using Structure.Player.Stack;
using UnityEngine;
using Utilities.Helpers;

namespace Project.MeshOpener
{
    public enum MeshOpenerType
    {
        None,
        Area,
        Machine
    }
    
    public class MeshOpenerBase : MonoBehaviour
    {
        [SerializeField] private MeshOpenerType meshOpenerType;
        [SerializeField] private int openCost;
        [SerializeField][Range(0.001f, 5f)] private float counterWaitTime = 0.1f;
        
        private PlayerInteractable _playerInteractable;
        private MeshOpenerUIController _uiController;
        private CurrencyController _currencyController;
        private PlayerBase _currentPlayer;
        private Coroutine _counterCoroutine;
        private int _remainingCost;
        private bool _isCounterActive;
        
        private void Awake()
        {
            _playerInteractable = GetComponentInChildren<PlayerInteractable>();
            _uiController = GetComponent<MeshOpenerUIController>();
            _playerInteractable.OnPlayerInteract += OnPlayerInteract;

            var cost = SaveManager.Instance.GetMeshRequiredAmount(meshOpenerType);
            _remainingCost = cost == -1 ? openCost: cost;
        }

        private void Start()
        {
            _currencyController = CurrencyController.Instance;
            _uiController.UpdateText(_remainingCost);
        }

        private IEnumerator ActivateCounter()
        {
            while (_isCounterActive && _remainingCost > 0)
            {
                yield return GeneralHelpers.GetWait(counterWaitTime);
                
                if (_currencyController.TryCharge(CurrencyType.Money, 1))
                    yield break;
                
                _remainingCost -= 1;
                SaveManager.Instance.SetMeshRequiredAmount(meshOpenerType, _remainingCost);
                _uiController.UpdateText(_remainingCost);
                _currentPlayer.ThrowCoin(transform);
            }
        }

        private void OnPlayerInteract(PlayerBase player, bool interacted)
        {
            if(_counterCoroutine != null)
                StopCoroutine(_counterCoroutine);
            
            if (interacted)
            {
                _currentPlayer = player;
                _isCounterActive = true;
                _counterCoroutine = StartCoroutine(ActivateCounter());
                return;
            }
            
            _isCounterActive = false;
        }

        private void OnDestroy()
        {
            _playerInteractable.OnPlayerInteract -= OnPlayerInteract;
        }
    }
}