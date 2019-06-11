using System;
using System.Linq;

using Apteka.Model.Annotations;
using Apteka.Model.Extensions;

using DevExpress.ExpressApp.Model;

namespace Apteka.Module.Extensions
{
    public static class IModelListViewExtensions
    {
        public static void UpdateColumnCaptions(this IModelListView view)
        {
            foreach (var col in view.Columns)
            {
                var mi = col.ModelMember.MemberInfo;
                var prop = mi.Owner.Type.GetProperty(mi.Name);
                var memberAttr = prop.GetCustomAttributes(typeof(DataElementAttribute), false)
                    .OfType<DataElementAttribute>().FirstOrDefault();
                if (memberAttr != null)
                {
                    if (!String.IsNullOrWhiteSpace(memberAttr.BriefName))
                    {
                        col.Caption = memberAttr.BriefName.FirstCharToUpper();
                    }
                    else
                    {
                        col.Caption = memberAttr.Name.FirstCharToUpper();
                    }
                }
            }
        }
    }
}
