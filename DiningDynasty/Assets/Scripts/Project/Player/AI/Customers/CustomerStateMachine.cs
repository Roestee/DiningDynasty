using Project.Player.AI.Customers.States;
using Structure.Player.AI;

namespace Project.Player.AI.Customers
{
    public class CustomerStateMachine : StateMachineBase
    {
        private Customer _customer;

        private void Awake()
        {
            _customer = GetComponent<Customer>();
        }
        
        public override void SetDefaultState()
        {
            SetState(new LookingForTableState(_customer));
        }

        public override void SetStartState()
        {
            
        }
    }
}