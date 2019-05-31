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
//            modelBuilder.Entity<Model.Entities.Organization>()
//                .HasOptional(e => e.Address)
//                .WithOptionalPrincipal()
//                .WillCascadeOnDelete();
//        }
//    }
//}
