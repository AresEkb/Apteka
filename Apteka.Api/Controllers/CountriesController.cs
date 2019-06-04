using Apteka.Api.Annotations;
using Apteka.Api.Controllers.Base;
using Apteka.Model.EFCore;
using Apteka.Model.Entities.Place;

namespace Apteka.Api.Controllers
{
    [EntityController(typeof(Country))]
    public class CountriesController : EntityController<AptekaDbContext, Country>
    {
        public CountriesController(AptekaDbContext context) : base(context) { }
    }
}
