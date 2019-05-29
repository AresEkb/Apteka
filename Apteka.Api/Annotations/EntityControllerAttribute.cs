using System;

namespace Apteka.Api.Annotations
{
    public class EntityControllerAttribute : Attribute
    {
        public EntityControllerAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}
