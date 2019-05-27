namespace Apteka.Model.Factories
{
    public interface IEntityFactory
    {
        T Create<T>() where T : new();
    }
}
