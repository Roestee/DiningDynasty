using Structure.Player.AI;

namespace Project.Player.AI.Customers.States
{
    public abstract class CustomerStateBase : StateBase
    {
        protected readonly Customer Customer;

        protected CustomerStateBase(Customer customer)
        {
            Customer = customer;
        }
    }
}