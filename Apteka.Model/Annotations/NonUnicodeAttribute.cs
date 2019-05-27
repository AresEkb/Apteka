using System;

namespace Apteka.Model.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NonUnicodeAttribute : Attribute
    {
    }
}
