using System.Linq;

namespace Apteka.Model.Factories
{
    public interface IEntityFactory
    {
        T Create<T>() where T : new();
        IQueryable<T> Query<T>();
    }
}
