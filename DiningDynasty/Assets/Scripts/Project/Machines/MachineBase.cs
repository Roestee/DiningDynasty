using System.Collections;
using Structure.Player.Stack;
using Structure.Player.Stack.StackTakers;
using UnityEngine;
using Utilities.Helpers;

namespace Project.Machines
{
    [RequireComponent(typeof(MachineEffectController), typeof(MachineProgressUIController))]
    public class MachineBase : MonoBehaviour
    {
        [SerializeField] private StackTakerBase stackTaker;
        [SerializeField] private Transform dummyStackTf;
        [SerializeField] private float workTime = 3f;
        [SerializeField] private float workAfterWaitTime = 0.5f;
        
        private Animator _anim;
        private MachineEffectController _effectController;
        private MachineProgressUIController _progressUiController;
        private bool _isWorking;
        
        private static readonly int Work = Animator.StringToHash("Work");
        private static readonly int Empty = Animator.StringToHash("Empty");

        protected virtual void Awake()
        {
            _anim = GetComponent<Animator>();
            _effectController = GetComponent<MachineEffectController>();
            _progressUiController = GetComponent<MachineProgressUIController>();
            
            stackTaker.OnCountChange += OnStackCountChange;
        }

        private void OnStackCountChange(int count, int capacity)
        {
            if (_isWorking)
                return;

            _isWorking = true;
            StartCoroutine(WorkCoroutine());
        }

        private IEnumerator WorkCoroutine()
        {
            while (_isWorking)
            {
                var stack = stackTaker.GetStack();
                if (stack == null)
                {
                    Debug.LogError("There is no stack available!");
                    break;
                }

                yield return StartCoroutine(stack.JumToPos(dummyStackTf.position, dummyStackTf.rotation));
                
                var pooledStack = (PooledPlayerStack)stack;
                pooledStack.PushToPool();
                _anim.SetTrigger(Work);
                _effectController.SetActiveCookingEffects();
                _progressUiController.StartProgress(workTime);
                
                yield return GeneralHelpers.GetWait(workTime);
                
                _anim.SetTrigger(Empty);
                _effectController.SetActiveCookingEffects(false);
                _effectController.PlayCookedEffect();

                yield return GeneralHelpers.GetWait(workAfterWaitTime);
            }

            _isWorking = false;
        }

        protected virtual void OnDestroy()
        {
            stackTaker.OnCountChange -= OnStackCountChange;
        }
    }
}