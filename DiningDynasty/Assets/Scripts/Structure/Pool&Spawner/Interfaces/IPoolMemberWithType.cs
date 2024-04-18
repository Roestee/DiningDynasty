namespace Structure.Pool_Spawner.Interfaces
{
    public interface IPoolMemberWithType<out T> : IPoolMemberBase
    {
        //Don't change the name, if you change pools manager will give you error
        T GetTypeForPool();
    }
}