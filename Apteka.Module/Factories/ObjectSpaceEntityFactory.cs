using System.Linq;

using Apteka.Model.Factories;

using DevExpress.ExpressApp;

namespace Apteka.Module.Factories
{
    public class ObjectSpaceEntityFactory : IEntityFactory
    {
        private readonly IObjectSpace os;

        public ObjectSpaceEntityFactory(IObjectSpace os)
        {
            this.os = os;
        }

        public T Create<T>() where T : new() => os.CreateObject<T>();

        public IQueryable<T> Query<T>() where T : class => os.GetObjectsQuery<T>(true);
    }
}
