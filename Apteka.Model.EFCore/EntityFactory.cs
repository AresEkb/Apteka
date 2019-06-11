using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Apteka.Model.Factories;

using Microsoft.EntityFrameworkCore;

namespace Apteka.Model.EFCore
{
    public class EntityFactory : IEntityFactory
    {
        private DbContext _context;
        private readonly HashSet<Type> cached = new HashSet<Type>();
        private readonly List<object> _cache = new List<object>();

        public EntityFactory(DbContext context)
        {
            _context = context;
        }

        public T Find<T>(Expression<Func<T, bool>> pred, EntitySource entitySource = EntitySource.Both) where T : class, new()
        {
            T entity = null;
            if (entitySource == EntitySource.Cache && !cached.Contains(typeof(T)))
            {
                CacheAll<T>();
                cached.Add(typeof(T));
            }
            if (entitySource == EntitySource.Both || entitySource == EntitySource.Cache)
            {
                entity = _cache.OfType<T>().AsQueryable().FirstOrDefault(pred);
                if (entity != null) { return entity; }
            }
            if (entitySource == EntitySource.Both || entitySource == EntitySource.DataBase)
            {
                entity = Query<T>().FirstOrDefault(pred);
            }
            return entity;
        }

        public T Create<T>() where T : new()
        {
            var entity = new T();
            _cache.Add(entity);
            return entity;
        }

        public void Attach<T>(T entity) where T : class => _context.Update(entity);

        public IQueryable<T> Query<T>() where T : class => _context.Set<T>();

        public void CacheAll<T>() where T : class => _cache.AddRange(_context.Set<T>());
    }
}
