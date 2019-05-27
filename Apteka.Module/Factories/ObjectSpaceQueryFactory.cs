using System.Linq;

using Apteka.Model.Factories;

using DevExpress.ExpressApp;

namespace Apteka.Module.Factories
{
    public class ObjectSpaceQueryFactory : IQueryFactory
    {
        private readonly IObjectSpace os;

        public ObjectSpaceQueryFactory(IObjectSpace os)
        {
            this.os = os;
        }

        public IQueryable<T> Create<T>() => os.GetObjectsQuery<T>(true);
    }
}
