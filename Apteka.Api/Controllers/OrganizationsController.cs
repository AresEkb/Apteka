using Apteka.Api.Annotations;
using Apteka.Api.Controllers.Base;
using Apteka.Model.Entities;

namespace Apteka.Api.Controllers
{
    [EntityController(typeof(Organization))]
    public class OrganizationsController : EntityController<AptekaDbContext, Organization>
    {
        public OrganizationsController(AptekaDbContext context) : base(context) { }
    }
}
