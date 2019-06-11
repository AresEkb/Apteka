using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Apteka.Model.Annotations;
using Apteka.Model.Extensions;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Persistent.Validation;

namespace Apteka.Module.ModelExtensions
{
    public class ModelMemberUpdater : ModelNodesGeneratorUpdater<ModelBOModelMemberNodesGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            var members = (IModelBOModelClassMembers)node;
            var application = (ModelApplicationBase)node.Application;
            string currentAspect = application.GetAspect(application.GetCurrentAspectIndex());
            try
            {
                application.SetCurrentAspect("ru");
                foreach (var m in members)
                {
                    var prop = m.MemberInfo.Owner.Type.GetProperty(m.MemberInfo.Name);

                    // Update caption
                    var attr = prop.GetCustomAttributes<DataElementAttribute>(false).FirstOrDefault();
                    if (attr != null)
                    {
                        m.Caption = attr.Name.FirstCharToUpper();
                    }

                    // Update display format and edit mask
                    var formatAttr = prop.GetCustomAttributes<DisplayFormatAttribute>(false).FirstOrDefault();
                    if (formatAttr != null && !String.IsNullOrWhiteSpace(formatAttr.DataFormatString))
                    {
                        m.DisplayFormat = formatAttr.DataFormatString;
                        if (formatAttr.ApplyFormatInEditMode)
                        {
                            m.EditMask = formatAttr.DataFormatString
                                .Substring(3, formatAttr.DataFormatString.Length - 4);
                        }
                    }
                    else if (prop.PropertyType == typeof(decimal) ||
                        Nullable.GetUnderlyingType(prop.PropertyType) == typeof(decimal))
                    {
                        m.DisplayFormat = "{0:N}";
                        m.EditMask = "n";
                    }

                    // Add range rule
                    var rangeAttr = prop.GetCustomAttributes<RangeAttribute>(false).FirstOrDefault();
                    if (rangeAttr != null)
                    {
                        m.MemberInfo.AddAttribute(
                            new RuleRangeAttribute(rangeAttr.Minimum, rangeAttr.Maximum));
                    }
                }
            }
            finally
            {
                application.SetCurrentAspect(currentAspect);
            }
        }
    }
}
