namespace Apteka.Model
{
    public interface IEntityFactory
    {
        T Create<T>() where T : new();
    }
}
