using System.Linq;

using Apteka.Model.Factories;

using Microsoft.EntityFrameworkCore;

namespace Apteka.Api.Factories
{
    public class EntityFrameworkEntityFactory : IEntityFactory
    {
        DbContext _context;

        public EntityFrameworkEntityFactory(DbContext context)
        {
            _context = context;
        }

        public T Create<T>() where T : new() => new T();

        public IQueryable<T> Query<T>() where T : class => _context.Set<T>();
    }
}
