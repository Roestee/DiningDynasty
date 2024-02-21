using System;

namespace Structure.GenericObjectPooling.Abstracts
{
    public interface IPoolMember
    {
        event Action<IPoolMember> OnDeath;
        void OnCreate();
        void OnEnterPool();
        void OnExitPool();
    }
}