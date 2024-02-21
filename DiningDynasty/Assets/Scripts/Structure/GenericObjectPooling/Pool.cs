using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Structure.GenericObjectPooling.Abstracts;

namespace Structure.GenericObjectPooling
{
    public class Pool<T> : IEnumerable where T: class, IPoolMember 
    {
        private List<T> _members = new List<T>();
        private HashSet<T> _unavailable = new HashSet<T>();
        private IFactory<T> _factory;

        private const int DefaultPoolSize = 5;

        public Pool(IFactory<T> factory, int poolSize = DefaultPoolSize) 
        {
            _factory = factory;

            for(var i = 0; i < poolSize; i++)
                Create();
        }

        public T Pull() 
        {
            foreach (var member in _members.Where(m => !_unavailable.Contains(m)))
            {
                HandleMemberOnPull(member);
                return member;
            }
            
            var newMember = Create();
            HandleMemberOnPull(newMember);
            return newMember;
        }

        private void HandleMemberOnPull(T member)
        {
            _unavailable.Add(member);
            member.OnExitPool();
            member.OnDeath += OnMemberDeath;
        }

        private void OnMemberDeath(IPoolMember member)
        {
            member.OnDeath -= OnMemberDeath;       
            Push(member as T);
        }

        public void Push(T member)
        {
            member.OnEnterPool();
            _unavailable.Remove(member);
        }

        public void AddExternalMember(T member)
        {
            _members.Add(member);
        }

        private T Create()
        {
            var member = _factory.Create();
            member.OnCreate();
            _members.Add(member);
            return member;
        }

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return _members.GetEnumerator();
        }
    }
}
