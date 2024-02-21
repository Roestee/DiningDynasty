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
        protected List<StackCell> Cells;
        private PlayerBase _currentInteract;

        protected abstract void Init();

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

                var cell = Cells.FirstOrDefault(p => p.CurrentStack == null);
                if (cell == null)
                    yield break;

                cell.CurrentStack = stack;
                stack.JumpToCell(cell);
                yield return GeneralHelpers.GetWait(stackTakeDuration);
            }
        }
    }
}