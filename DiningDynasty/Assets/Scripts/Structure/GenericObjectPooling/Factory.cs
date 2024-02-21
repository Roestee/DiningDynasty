using Structure.GenericObjectPooling.Abstracts;

namespace Structure.GenericObjectPooling
{
    public class Factory<T> : IFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }

}