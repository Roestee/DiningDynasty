namespace Project.Player.AI.Customers.States
{
    public class IdleState: CustomerStateBase
    {
        public IdleState(Customer customer) : base(customer)
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