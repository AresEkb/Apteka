using Apteka.Api.Annotations;
using Apteka.Api.Controllers.Base;
using Apteka.Model.EFCore;
using Apteka.Model.Entities.Place;

namespace Apteka.Api.Controllers
{
    [EntityController(typeof(City))]
    public class CitiesController : EntityController<AptekaDbContext, City>
    {
        public CitiesController(AptekaDbContext context) : base(context) { }
    }
}
