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
            
        }

        public override void SetStartState()
        {
            
        }
    }
}