using Project.Tables;

namespace Project.Player.AI.Customers.States
{
    public class GoToTableState : CustomerStateBase
    {
        private bool _isArrive;
        private Chair _currentChair;
        
        private readonly CustomerTable _currentTable;
        private const float StopDistance = 0.01f;

        public GoToTableState(Customer customer, CustomerTable table) : base(customer)
        {
            _currentTable = table;
        }

        protected override void DoOnStateStart()
        {
            GoToTable();
        }

        protected override void DoOnStateEnd()
        {
            
        }

        public override void DoState()
        {
            if (_isArrive)
                return;
            
            CheckIsArrive();
        }

        private void GoToTable()
        {
            _currentChair = _currentTable.GetAvailableChair();
            _currentChair.CurrentCustomer = Customer;
            
            Customer.HandleMovement(_currentChair.transform.position);
        }

        private void CheckIsArrive()
        {
            var distanceSqr = (_currentChair.transform.position - Customer.transform.position).sqrMagnitude;
            if (distanceSqr > StopDistance)
                return;
            
            _isArrive = true;
            Customer.StopMovement();
            Customer.SitToChair(_currentChair);
        }
    }
}