namespace Apteka.Model
{
    public class PlainEntityFactory : IEntityFactory
    {
        public T Create<T>() where T : new()
        {
            return new T();
        }
    }
}
