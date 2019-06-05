using System;
using System.Linq;

namespace Apteka.Model.Factories
{
    public interface IEntityFactory
    {
        T Find<T>(Func<T, bool> pred) where T : class, new();
        T Create<T>() where T : new();
        void Attach<T>(T entity) where T : class;
        IQueryable<T> Query<T>() where T : class;
    }
}
