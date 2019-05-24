namespace Apteka.Model
{
    public interface IObjectFactory
    {
        T Create<T>() where T : new();
    }
}
