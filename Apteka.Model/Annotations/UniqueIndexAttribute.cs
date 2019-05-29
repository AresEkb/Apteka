using System;

namespace Apteka.Model.Annotations
{
    // TODO: Rename to index, because alternate keys should be read-only
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UniqueIndexAttribute : Attribute
    {
        public UniqueIndexAttribute()
        {
            Name = null;
            Order = -1;
        }

        public UniqueIndexAttribute(string name)
        {
            Name = name;
            Order = -1;
        }

        public UniqueIndexAttribute(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; private set; }
        public int Order { get; private set; }
    }
}
