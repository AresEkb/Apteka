using System;
using System.Linq;

namespace Apteka.Model.Factories
{
    public class PlainEntityFactory : IEntityFactory
    {
        public T Create<T>() where T : new()
        {
            return new T();
        }

        public IQueryable<T> Query<T>()
        {
            throw new NotImplementedException();
        }
    }
}
