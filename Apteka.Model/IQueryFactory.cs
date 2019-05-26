using System.Linq;

namespace Apteka.Model
{
    public interface IQueryFactory
    {
        IQueryable<T> Create<T>();
    }
}
