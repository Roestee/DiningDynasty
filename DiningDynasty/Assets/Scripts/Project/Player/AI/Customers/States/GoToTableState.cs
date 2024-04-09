using Project.Tables;

namespace Project.Player.AI.Customers.States
{
    public class GoToTableState : CustomerStateBase
    {
        private readonly CustomerTable _currentTable;
        
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
            
        }

        private void GoToTable()
        {
            var chair = _currentTable.GetAvailableChair();
            chair.CurrentCustomer = Customer;
            
            Customer.HandleMovement(chair.transform.position);
        }
    }
}