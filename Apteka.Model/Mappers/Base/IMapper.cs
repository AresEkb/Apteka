using Apteka.Model.Entities.Base;

namespace Apteka.Model.Mappers.Base
{
    public interface IMapper<S, T>
        where T : IEntity
    {
        T Map(S source);
    }
}
