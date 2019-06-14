using System.ComponentModel;
using System.Reflection;

using Apteka.Model.Annotations;
using Apteka.Model.Extensions;

namespace Apteka.Model.Entities.Base
{
    public abstract class EntityBase
    {
        public override string ToString()
        {
            var type = GetType();
            var attr = type.GetCustomAttribute<DataElementAttribute>();
            if (attr != null)
            {
                var defPropAttr = type.GetCustomAttribute<DefaultPropertyAttribute>();
                var prop = defPropAttr != null
                    ? type.GetProperty(defPropAttr.Name)
                    : type.GetProperty("Name") ?? GetType().GetProperty("Id");
                return attr.Name.FirstCharToUpper() + " (" + prop.GetValue(this) + ")";
            }
            return base.ToString();
        }
    }
}
