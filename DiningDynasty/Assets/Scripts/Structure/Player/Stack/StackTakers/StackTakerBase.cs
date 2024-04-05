using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Helpers;

namespace Structure.Player.Stack.StackTakers
{
    public abstract class StackTakerBase : MonoBehaviour
    {
        public event Action<int, int> OnCountChange;

        [SerializeField] protected StackCell cellPrefab;
        [SerializeField] protected Transform cellsParent;
        [Header("Init Options")]
        [SerializeField] protected int columnCount = 4;
        [SerializeField] protected float distanceBetweenColumn = 0.3f;

        [Header("Stack Take")] 
        [SerializeField] protected bool isTypeTaker;
        [HideIf("@!isTypeTaker")]
        [SerializeField] protected PlayerStackType takeStackType;
        [SerializeField] protected float stackTakeDuration = 0.2f;

        private PlayerInteractable _takerInteractable;
        private PlayerBase _currentInteract;
        
        protected List<StackCell> Cells;
        protected abstract void Init();
        
        public int GetStackCount() => Cells.Count(p => p.CurrentStack != null);
        public int GetCapacity() => Cells.Count;
        public PlayerStackType GetStackType() => takeStackType;

        private void Awake()
        {
            _takerInteractable = GetComponentInChildren<PlayerInteractable>(true);
            _takerInteractable.OnPlayerInteract += OnTakerInteract;

            takeStackType = isTypeTaker ? takeStackType : PlayerStackType.None;
            Init();
        }

        private void OnTakerInteract(PlayerBase player, bool isInteracted)
        {
            _currentInteract = isInteracted ? player: null;

            if(isInteracted)
                StartCoroutine(TakeStackCoroutine());
        }

        private IEnumerator TakeStackCoroutine()
        {
            while (_currentInteract != null)
            {
                var stack = _currentInteract.GetStack(takeStackType);
                if(stack == null)
                    yield break;

                if (!ThrowStack(stack))
                    yield break;
                
                yield return GeneralHelpers.GetWait(stackTakeDuration);
            }
        }

        public bool ThrowStack(PlayerStack stack)
        {
            var cell = Cells.FirstOrDefault(p => p.CurrentStack == null);
            if (cell == null)
                return false;

            cell.CurrentStack = stack;
            stack.JumpToCell(cell);
            OnCountChange?.Invoke(Cells.Count(p=>p.CurrentStack != null), Cells.Count);
            return true;
        }

        public PlayerStack GetStack()
        {
            var cell = Cells.LastOrDefault(p => p.CurrentStack != null);
            if (cell == null)
                return null;

            var stack = cell.CurrentStack;
            cell.CurrentStack = null;
            OnCountChange?.Invoke(Cells.Count(p=>p.CurrentStack != null), Cells.Count);
            return stack;
        }
    }
}