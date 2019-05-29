using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Apteka.Api.Annotations;
using Apteka.Model.Annotations;
using Apteka.Model.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json.Serialization;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Apteka.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void IncludeModelAnnotations(this SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.SchemaFilter<ModelAnnotationsSchemaFilter>();
            swaggerGenOptions.DocumentFilter<ModelAnnotationsDocumentFilter>();
        }
    }

    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerGen/XmlComments/XmlCommentsSchemaFilter.cs
    public class ModelAnnotationsSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var jsonObjectContract = context.JsonContract as JsonObjectContract;
            if (jsonObjectContract == null) return;

            var attr = context.SystemType.GetCustomAttribute<DataElementAttribute>();
            if (attr != null && !String.IsNullOrWhiteSpace(attr.Name))
            {
                schema.Description = attr.Name.FirstCharToUpper();
            }

            if (schema.Properties == null) return;
            foreach (var entry in schema.Properties)
            {
                if (!jsonObjectContract.Properties.Contains(entry.Key))
                {
                    continue;
                }
                var jsonProperty = jsonObjectContract.Properties[entry.Key];

                if (jsonProperty.TryGetMemberInfo(out MemberInfo memberInfo))
                {
                    ApplyPropertyComments(entry.Value, memberInfo);
                }
            }
        }

        private void ApplyPropertyComments(Schema propertySchema, MemberInfo memberInfo)
        {
            var attr = memberInfo.GetCustomAttribute<DataElementAttribute>();
            if (attr != null && !String.IsNullOrWhiteSpace(attr.Name)) {
                propertySchema.Description = attr.Name.FirstCharToUpper();
            }
        }
    }

    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerGen/XmlComments/XmlCommentsDocumentFilter.cs
    public class ModelAnnotationsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var controllerNamesAndTypes = context.ApiDescriptions
                .Select(apiDesc => apiDesc.ActionDescriptor as ControllerActionDescriptor)
                .SkipWhile(actionDesc => actionDesc == null)
                .GroupBy(actionDesc => actionDesc.ControllerName)
                .Select(group => new KeyValuePair<string, Type>(group.Key, group.First().ControllerTypeInfo.AsType()));

            foreach (var nameAndType in controllerNamesAndTypes)
            {
                var ctrAttr = nameAndType.Value.GetCustomAttribute<EntityControllerAttribute>();
                if (ctrAttr != null)
                {
                    var attr = ctrAttr.Type.GetCustomAttribute<DataElementAttribute>();
                    if (attr != null)
                    {
                        if (swaggerDoc.Tags == null)
                        {
                            swaggerDoc.Tags = new List<Tag>();
                        }
                        swaggerDoc.Tags.Add(new Tag
                        {
                            Name = nameAndType.Key,
                            Description = attr.PluralName.FirstCharToUpper()
                        });
                    }
                }
            }
        }
    }

    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerGen/SchemaGen/JsonPropertyExtensions.cs
    internal static class JsonPropertyExtensions
    {
        internal static bool TryGetMemberInfo(this JsonProperty jsonProperty, out MemberInfo memberInfo)
        {
            if (jsonProperty.UnderlyingName == null)
            {
                memberInfo = null;
                return false;
            }

            var metadataAttribute = jsonProperty.DeclaringType
                .GetCustomAttributes(typeof(ModelMetadataTypeAttribute), true)
                .FirstOrDefault();

            var typeToReflect = (metadataAttribute != null)
                ? ((ModelMetadataTypeAttribute)metadataAttribute).MetadataType
                : jsonProperty.DeclaringType;

            memberInfo = typeToReflect.GetMember(jsonProperty.UnderlyingName).FirstOrDefault();

            return (memberInfo != null);
        }
    }
}
