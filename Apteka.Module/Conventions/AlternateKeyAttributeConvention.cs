using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

using Apteka.Model.Annotations;

namespace Apteka.Module.Conventions
{
    public class AlternateKeyAttributeConvention :
        PrimitivePropertyAttributeConfigurationConvention<AlternateKeyAttribute>
    {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, AlternateKeyAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.Name))
            {
                configuration.HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            }
            else if (attribute.Order < 0)
            {
                configuration.HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(attribute.Name) { IsUnique = true }));
            }
            else
            {
                configuration.HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(attribute.Name, attribute.Order) { IsUnique = true }));
            }
        }
    }
}
