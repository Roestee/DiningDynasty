using System;

namespace Structure.Pool_Spawner.PoolLogics
{
    public abstract class PoolLogicBase<T>
    {
        public abstract void Push(T member);
        
        protected readonly Action<T> OnEnterPool;
        protected readonly Action<T> OnExitPool;

        protected PoolLogicBase(Action<T> onEnterPool, Action<T> onExitPool)
        {
            OnEnterPool = onEnterPool;
            OnExitPool = onExitPool;
        }
    }
}