using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

using Apteka.Model.Annotations;

namespace Apteka.Module.Conventions
{
    public class NonUnicodeAttributeConvention :
        PrimitivePropertyAttributeConfigurationConvention<NonUnicodeAttribute>
    {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, NonUnicodeAttribute attribute)
        {
            configuration.IsUnicode(false);
        }
    }
}
