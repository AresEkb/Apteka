namespace Apteka.Model.Factories
{
    public class PlainEntityFactory : IEntityFactory
    {
        public T Create<T>() where T : new()
        {
            return new T();
        }
    }
}
