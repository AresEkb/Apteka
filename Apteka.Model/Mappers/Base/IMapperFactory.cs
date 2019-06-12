using Apteka.Model.Entities.Base;
using Apteka.Model.Factories;

namespace Apteka.Model.Mappers.Base
{
    public interface IMapperFactory<S, T>
        where T : IEntity
    {
        IMapper<S, T> Create(IEntityFactory entityFactory, bool updateExisting = false, bool readOnlyFromCache = false);
    }
}
