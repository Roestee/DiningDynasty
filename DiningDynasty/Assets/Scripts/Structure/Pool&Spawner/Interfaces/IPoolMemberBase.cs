namespace Structure.Pool_Spawner.Interfaces
{
    public interface IPoolMemberBase
    {
        void OnCreate();
        void OnEnterPool();
        void OnExitPool();
    }
}