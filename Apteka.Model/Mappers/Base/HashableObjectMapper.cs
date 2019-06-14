using System.Collections.Generic;
using System.Linq;

using Apteka.Model.Dtos.Base;
using Apteka.Model.Entities.Base;
using Apteka.Model.Factories;

namespace Apteka.Model.Mappers.Base
{
    public abstract class HashableObjectMapper<S, T> : MapperBase, IMapper<S, T>
        where S : IHashable
        where T : class, IHashableEntity, IEntity, new()
    {
        private readonly bool updateExisting;
        private readonly bool readOnlyFromCache;
        private readonly HashSet<long> keys;

        protected HashableObjectMapper(IEntityFactory entityFactory,
            bool updateExisting = false, bool readOnlyFromCache = false)
            : base(entityFactory, readOnlyFromCache ? EntitySource.Cache : EntitySource.Both)
        {
            this.updateExisting = updateExisting;
            this.readOnlyFromCache = readOnlyFromCache;

            if (!updateExisting)
            {
                keys = new HashSet<long>(
                    EntityFactory.Query<T>()
                        .Where(df => df.Hash.HasValue)
                        .Select(df => df.Hash.Value));
            }
        }

        public T Map(S dto)
        {
            long hash = dto.Hash;

            T entity = null;
            // If we will update records then we have to load all columns
            if (updateExisting)
            {
                entity = FindOrCreate<T>(e => e.Hash == hash, updateExisting);
            }
            // If we will not update records then it's much more effective to load keys only
            else if (!keys.Contains(hash))
            {
                entity = EntityFactory.Create<T>();
                entity.Hash = hash;
                keys.Add(hash);
            }

            if (entity == null) { return null; }

            MapProperties(dto, entity);

            return entity;
        }

        protected abstract void MapProperties(S dto, T entity);
    }
}
