namespace Structure.GenericObjectPooling.Abstracts
{
    public interface IFactory<out T>
    {
        T Create();
    }
}