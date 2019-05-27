using System.Linq;

namespace Apteka.Model.Factories
{
    public interface IQueryFactory
    {
        IQueryable<T> Create<T>();
    }
}
