﻿using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

using Apteka.Model.Annotations;

namespace Apteka.Module.Conventions
{
    //public class CompositionAttributeConvention :
    //    PrimitivePropertyAttributeConfigurationConvention<CompositionAttribute>
    //{
    //    public override void Apply(ConventionPrimitivePropertyConfiguration configuration, CompositionAttribute attribute)
    //    {
    //        modelBuilder.Entity<Model.Entities.Organization>()
    //            .HasOptional(e => e.Address)
    //            .WithOptionalPrincipal()
    //            .WillCascadeOnDelete();
    //    }
    //}

    //public class CompositionAttributeConvention : IStoreModelConvention<AssociationType>
    //{
    //    public void Apply(AssociationType association, DbModel model)
    //    {
    //        // Identify a ForeignKey properties (including IAs)
    //        if (association.IsForeignKey)
    //        {
    //            // rename FK columns
    //            var constraint = association.Constraint;
    //            if (DoPropertiesHaveDefaultNames(constraint.FromProperties, constraint.ToRole.Name, constraint.ToProperties))
    //            {
    //                NormalizeForeignKeyProperties(constraint.FromProperties);
    //            }
    //            if (DoPropertiesHaveDefaultNames(constraint.ToProperties, constraint.FromRole.Name, constraint.FromProperties))
    //            {
    //                NormalizeForeignKeyProperties(constraint.ToProperties);
    //            }
    //        }
    //    }

    //    private bool DoPropertiesHaveDefaultNames(ReadOnlyMetadataCollection<EdmProperty> properties, string roleName, ReadOnlyMetadataCollection<EdmProperty> otherEndProperties)
    //    {
    //        if (properties.Count != otherEndProperties.Count)
    //        {
    //            return false;
    //        }

    //        for (int i = 0; i < properties.Count; ++i)
    //        {
    //            if (!properties[i].Name.EndsWith("_" + otherEndProperties[i].Name))
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }

    //    private void NormalizeForeignKeyProperties(ReadOnlyMetadataCollection<EdmProperty> properties)
    //    {
    //        for (int i = 0; i < properties.Count; ++i)
    //        {
    //            string defaultPropertyName = properties[i].Name;
    //            int ichUnderscore = defaultPropertyName.IndexOf('_');
    //            if (ichUnderscore <= 0)
    //            {
    //                continue;
    //            }
    //            string navigationPropertyName = defaultPropertyName.Substring(0, ichUnderscore);
    //            string targetKey = defaultPropertyName.Substring(ichUnderscore + 1);

    //            string newPropertyName;
    //            if (targetKey.StartsWith(navigationPropertyName))
    //            {
    //                newPropertyName = targetKey;
    //            }
    //            else
    //            {
    //                newPropertyName = navigationPropertyName + targetKey;
    //            }
    //            properties[i].Name = newPropertyName;
    //        }
    //    }
    //}
}
