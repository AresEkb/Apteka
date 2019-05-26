using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace Apteka.Module.Conventions
{
    // HACK: We have to override MaxLengthAttributeConvention
    // because IsFixedLength() must be called before HasMaxLength()
    // Otherwise it is ignored
    public class CustomMaxLengthAttributeConvention :
        PrimitivePropertyAttributeConfigurationConvention<MaxLengthAttribute>
    {
        private const int MaxLengthIndicator = -1;

        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, MaxLengthAttribute attribute)
        {
            var memberInfo = configuration.ClrPropertyInfo;
            if (attribute.Length == 0 || attribute.Length < MaxLengthIndicator)
            {
                throw new ArgumentException("MaxLength must be a positive number or -1.");
            }

            if (attribute.Length == MaxLengthIndicator)
            {
                configuration.IsMaxLength();
            }
            else
            {
                var minAttr = memberInfo.GetCustomAttributes<MinLengthAttribute>(false).FirstOrDefault();
                if (minAttr != null)
                {
                    configuration.IsFixedLength().HasMaxLength(attribute.Length);
                }
                else
                {
                    configuration.HasMaxLength(attribute.Length);
                }
            }
        }
    }
}
