using System;
using System.Collections.Generic;
using Structure.Pool_Spawner.Interfaces;

namespace Structure.Pool_Spawner.PoolLogics
{
    public sealed class PoolLogicSimple<T> : PoolLogicBase<T> where T : IPoolMemberBase
    {
        private readonly Stack<T> _objectStack = new Stack<T>();
        private readonly Func<T> _onCreate;

        public int GetObjectsCount() => _objectStack.Count;
        public Stack<T> GetAllObjects() => _objectStack;

        public PoolLogicSimple(Func<T> onCreate, Action<T> onEnterPool, Action<T> onExitPool) : base(onEnterPool, onExitPool)
        {
            _onCreate = onCreate;
        }

        public void CreateMultiple(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newMember = _onCreate();
                _objectStack.Push(newMember);
            }
        }

        public T Pull()
        {
            if (_objectStack.Count > 0)
            {
                var objectT = _objectStack.Pop();
                OnExitPool(objectT);
                return objectT;
            }

            var newMember = _onCreate();
            OnExitPool(newMember);
            return newMember;
        }
        
        public override void Push(T member)
        {
            _objectStack.Push(member);
            OnEnterPool(member);
        }
    }
}