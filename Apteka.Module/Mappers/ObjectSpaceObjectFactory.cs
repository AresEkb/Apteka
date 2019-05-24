using Apteka.Model;

using DevExpress.ExpressApp;

namespace Apteka.Module.Mappers
{
    public class ObjectSpaceObjectFactory : IObjectFactory
    {
        private readonly IObjectSpace os;

        public ObjectSpaceObjectFactory(IObjectSpace os)
        {
            this.os = os;
        }

        public T Create<T>() where T : new()
        {
            return os.CreateObject<T>();
        }
    }
}
