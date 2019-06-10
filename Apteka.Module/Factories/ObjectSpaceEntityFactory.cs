using System;
using System.Linq;
using System.Linq.Expressions;

using Apteka.Model.Factories;

using DevExpress.ExpressApp;

namespace Apteka.Module.Factories
{
    public class ObjectSpaceEntityFactory : IEntityFactory
    {
        private readonly IObjectSpace os;

        public ObjectSpaceEntityFactory(IObjectSpace os)
        {
            this.os = os;
        }

        public T Find<T>(Expression<Func<T, bool>> pred, EntitySource entitySource = EntitySource.Both) where T : class, new() =>
            Query<T>().FirstOrDefault(pred);

        public T Create<T>() where T : new() => os.CreateObject<T>();

        public void Attach<T>(T entity) where T : class
        {
        }

        public IQueryable<T> Query<T>() where T : class => os.GetObjectsQuery<T>(true);

        public void CacheAll<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
