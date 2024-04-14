using System.Collections;
using DG.Tweening;
using Sirenix.Utilities;
using Structure.Managers;
using Structure.Player;
using Structure.Player.Stack;
using UnityEngine;
using Utilities.Helpers;

namespace Project.MeshOpener
{
    public abstract class MeshOpenerBase<T> : MonoBehaviour
    {
        [Header("Unlock-Lock Objects")]
        [SerializeField] private GameObject unlockObject;
        [SerializeField] private GameObject lockObject;
   
        [Header("Cost")]
        [SerializeField] private int openCost;
        [Space]
        [SerializeField][Range(0.001f, 5f)] private float counterWaitTime = 0.1f;

        [Header("Animation")] 
        [SerializeField] private Ease meshOpenEase = Ease.OutBack;
        [SerializeField] private float meshOpenTime = 0.3f;
        [SerializeField] private bool throwCoin;
        
        [Header("Mesh Type")]
        [SerializeField] private MeshOpenerType meshOpenerType;
        [SerializeField] private int id;

        [Space]
        [SerializeField] private bool isDefaultOpen;
        
        private PlayerInteractable _playerInteractable;
        private MeshOpenerUIController _uiController;
        private CurrencyController _currencyController;
        private PlayerBase _currentPlayer;
        private Coroutine _counterCoroutine;
        private int _remainingCost;
        private bool _isCounterActive;
        private IOpenerMesh<T>[] _openerMeshes;

        protected abstract T GetSpecialType();
        public abstract void OnMeshOpen();
        
        protected virtual void Awake()
        {
            _playerInteractable = GetComponentInChildren<PlayerInteractable>();
            _openerMeshes = GetComponentsInChildren<IOpenerMesh<T>>(true);
            _uiController = GetComponent<MeshOpenerUIController>();
            
            _playerInteractable.OnPlayerInteract += OnPlayerInteract;
        }

        protected virtual void Start()
        {
            if (isDefaultOpen)
            {
                SetActiveUnlockMesh(true, true);
                return;
            }
            
            var cost = SaveManager.Instance.GetMeshRequiredAmount(meshOpenerType, id, GetSpecialType().ToString());
            if (cost == 0)
            {
                SetActiveUnlockMesh(true, true);
                return;
            }

            _remainingCost = cost == -1 ? openCost: cost;
            
            _currencyController = CurrencyController.Instance;
            _uiController.UpdateText(_remainingCost);
            
            SetActiveUnlockMesh(false);
        }

        private void SetActiveUnlockMesh(bool activate = true, bool instant = false)
        {
            unlockObject.SetActive(activate);
            lockObject.SetActive(!activate);

            if (!activate)
                return;
            
            _playerInteractable.OnPlayerInteract -= OnPlayerInteract;
            if (instant)
            {
                OnMeshOpen();
                _openerMeshes.ForEach(p => p.OnMeshOpen());
                return;
            }
            
            unlockObject.transform.DOScale(0, meshOpenTime)
                .From()
                .SetEase(meshOpenEase)
                .OnComplete(() =>
                {
                    OnMeshOpen();
                    _openerMeshes.ForEach(p => p.OnMeshOpen());
                });
        }

        private IEnumerator ActivateCounter()
        {
            while (_isCounterActive && _remainingCost > 0)
            {
                yield return GeneralHelpers.GetWait(counterWaitTime);
                
                if (!_currencyController.TryCharge(CurrencyType.Money, 1))
                    yield break;
                
                _remainingCost -= 1;
                SaveManager.Instance.SetMeshRequiredAmount(meshOpenerType, id, GetSpecialType().ToString(),_remainingCost);
                _uiController.UpdateText(_remainingCost);
                
                if(throwCoin)
                    _currentPlayer.ThrowCoin(transform);

                if (_remainingCost > 0) 
                    continue;
                
                SetActiveUnlockMesh();
                yield break;
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