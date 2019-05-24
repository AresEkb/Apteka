namespace Apteka.Model
{
    public class PlainObjectFactory : IObjectFactory
    {
        public T Create<T>() where T : new()
        {
            return new T();
        }
    }
}
