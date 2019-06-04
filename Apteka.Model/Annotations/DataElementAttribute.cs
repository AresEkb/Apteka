using System;

namespace Apteka.Model.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class DataElementAttribute : Attribute
    {
        public DataElementAttribute(string lang, string name)
        {
            Lang = lang;
            Name = name;
        }

        public DataElementAttribute(string lang, string name, string definition)
        {
            Lang = lang;
            Name = name;
            Definition = definition;
        }

        public DataElementAttribute(string lang, string name, string pluralName, string definition)
        {
            Lang = lang;
            Name = name;
            PluralName = pluralName;
            Definition = definition;
        }

        public string Lang { get; set; }
        public string Name { get; set; }
        public string BriefName { get; set; }
        public string PluralName { get; set; }
        public string Definition { get; set; }
    }
}
