using System;

namespace Apteka.Model.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AlternateKeyAttribute : Attribute
    {
        public AlternateKeyAttribute()
        {
            Name = null;
            Order = -1;
        }

        public AlternateKeyAttribute(string name)
        {
            Name = name;
            Order = -1;
        }

        public AlternateKeyAttribute(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; private set; }
        public int Order { get; private set; }
    }
}
