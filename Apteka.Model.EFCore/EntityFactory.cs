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
        private DbContext context;
        private readonly Dictionary<Type, object> cache = new Dictionary<Type, object>();

        public EntityFactory(DbContext context)
        {
            this.context = context;
        }

        public T Find<T>(Expression<Func<T, bool>> pred, EntitySource entitySource = EntitySource.Both) where T : class, new()
        {
            T entity = null;
            if (entitySource == EntitySource.Cache && !cache.ContainsKey(typeof(T)))
            {
                if (!cache.ContainsKey(typeof(T)))
                {
                    cache[typeof(T)] = new List<T>();
                }
                ((List<T>)cache[typeof(T)]).AddRange(context.Set<T>());
            }
            if (entitySource == EntitySource.Both || entitySource == EntitySource.Cache)
            {
                entity = ((List<T>)cache[typeof(T)]).AsQueryable().FirstOrDefault(pred);
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
            if (!cache.ContainsKey(typeof(T)))
            {
                cache[typeof(T)] = new List<T>();
            }
            ((List<T>)cache[typeof(T)]).Add(entity);
            return entity;
        }

        public void Attach<T>(T entity) where T : class => context.Update(entity);

        public IQueryable<T> Query<T>() where T : class => context.Set<T>();
    }
}
