namespace Structure.GenericObjectPooling.Abstracts
{
    public interface IPoolMember
    {
        void OnCreate();
        void OnEnterPool();
        void OnExitPool();
    }
}