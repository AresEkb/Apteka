using System;

namespace Apteka.Model
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NonUnicodeAttribute : Attribute
    {
    }
}
