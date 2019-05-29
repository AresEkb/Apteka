//using System;
//using System.Data.Entity.ModelConfiguration.Configuration;
//using System.Data.Entity.ModelConfiguration.Conventions;

//using Apteka.Model.Annotations;

//namespace Apteka.Module.Conventions
//{
//    public class CompositionAttributeConvention :
//        PrimitivePropertyAttributeConfigurationConvention<CompositionAttribute>
//    {
//        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, CompositionAttribute attribute)
//        {
//            System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention
//            configuration.will HasPrecision(attribute.Precision, attribute.Scale);
//        }
//    }
//}
