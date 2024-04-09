namespace Project.Player.AI.Customers
{
    public class Customer : AIBase
    {
        public CustomerStateMachine StateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            StateMachine = GetComponent<CustomerStateMachine>();
        }
    }
}