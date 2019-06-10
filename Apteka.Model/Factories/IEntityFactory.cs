using System;
using System.Linq;
using System.Linq.Expressions;

namespace Apteka.Model.Factories
{
    public enum EntitySource { Both, DataBase, Cache }

    public interface IEntityFactory
    {
        T Find<T>(Expression<Func<T, bool>> pred, EntitySource entitySource = EntitySource.Both) where T : class, new();
        T Create<T>() where T : new();
        void Attach<T>(T entity) where T : class;
        IQueryable<T> Query<T>() where T : class;
        void CacheAll<T>() where T : class;
    }
}
