using System;
using System.Linq;

namespace Apteka.Model.Factories
{
    public class PlainEntityFactory : IEntityFactory
    {
        public T Create<T>() where T : new() => new T();

        public IQueryable<T> Query<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
