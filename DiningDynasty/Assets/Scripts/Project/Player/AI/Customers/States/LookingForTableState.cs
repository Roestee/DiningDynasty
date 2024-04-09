using Project.Areas;
using UnityEngine;

namespace Project.Player.AI.Customers.States
{
    public class LookingForTableState: CustomerStateBase
    {
        private bool _canCheckForTable;
        private float _currentTime;
        
        private const float Timer = 2f;
        
        public LookingForTableState(Customer customer) : base(customer)
        {
        }

        protected override void DoOnStateStart()
        {
            LookForTable();
        }

        protected override void DoOnStateEnd()
        {
            
        }

        public override void DoState()
        {
            if (!_canCheckForTable)
                return;

            if (_currentTime >= Timer)
            {
                _canCheckForTable = false;
                LookForTable();
                return;
            }

            _currentTime += Time.deltaTime;
        }

        private void LookForTable()
        {
            var table = AreaManager.Instance.GetAvailableTable();
            if (table == null)
            {
                _currentTime = 0f;
                _canCheckForTable = true;
                return;
            }
            
            Customer.StateMachine.SetState(new GoToTableState(Customer, table));
        }
    }
}