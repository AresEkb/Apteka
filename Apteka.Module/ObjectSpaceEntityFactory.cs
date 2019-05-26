using Apteka.Model;

using DevExpress.ExpressApp;

namespace Apteka.Module.Mappers
{
    public class ObjectSpaceEntityFactory : IEntityFactory
    {
        private readonly IObjectSpace os;

        public ObjectSpaceEntityFactory(IObjectSpace os)
        {
            this.os = os;
        }

        public T Create<T>() where T : new()
        {
            return os.CreateObject<T>();
        }
    }
}
