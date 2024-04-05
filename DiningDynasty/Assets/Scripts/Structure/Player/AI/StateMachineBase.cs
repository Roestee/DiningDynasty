using Sirenix.OdinInspector;
using UnityEngine;

namespace Structure.Player.AI
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        private StateBase _currentState;
        
        public string stateName;

        [Button("Default State")]
        public abstract void SetDefaultState();
        [Button("Start State")]
        public abstract void SetStartState();

        public void SetDeadState()
        {
            _currentState?.OnStateEnd();
            _currentState = null;
        }
        
        public void SetState(StateBase stateBase)
        {
            _currentState?.OnStateEnd();
            _currentState = null;
            
            _currentState = stateBase;
            stateBase.OnStateStart();
            stateName = _currentState.ToString();
        }

        protected virtual void Update()
        {
            _currentState?.DoState();
            stateName = _currentState?.ToString();
        }

        private void OnDestroy()
        {
            SetDeadState();
        }
    }
}