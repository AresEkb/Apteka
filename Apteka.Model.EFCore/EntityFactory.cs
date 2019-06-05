using System;
using System.Collections.Generic;
using System.Linq;

using Apteka.Model.Factories;

using Microsoft.EntityFrameworkCore;

namespace Apteka.Model.EFCore
{
    public class EntityFactory : IEntityFactory
    {
        private DbContext _context;
        private readonly List<object> _cache = new List<object>();

        public EntityFactory(DbContext context)
        {
            _context = context;
        }

        public T Find<T>(Func<T, bool> pred) where T : class, new() =>
            _cache.OfType<T>().FirstOrDefault(pred) ??
            Query<T>().FirstOrDefault(pred);

        public T Create<T>() where T : new()
        {
            var entity = new T();
            _cache.Add(entity);
            return entity;
        }

        public void Attach<T>(T entity) where T : class => _context.Update(entity);

        public IQueryable<T> Query<T>() where T : class => _context.Set<T>();
    }
}
