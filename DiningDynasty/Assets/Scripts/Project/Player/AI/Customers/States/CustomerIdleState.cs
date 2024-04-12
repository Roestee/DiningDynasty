namespace Project.Player.AI.Customers.States
{
    public class CustomerIdleState: CustomerStateBase
    {
        public CustomerIdleState(Customer customer) : base(customer)
        {
        }

        protected override void DoOnStateStart()
        {
            Customer.StopMovement();
        }

        protected override void DoOnStateEnd()
        {
            
        }

        public override void DoState()
        {
            
        }
    }
}