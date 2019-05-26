using System;

namespace Apteka.Model
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class DataElementAttribute : Attribute
    {
        public DataElementAttribute(string lang, string name, string briefName, string pluralName, string definition)
        {
            Lang = lang;
            Name = name;
            BriefName = briefName;
            PluralName = pluralName;
            Definition = definition;
        }

        public DataElementAttribute(string lang, string name, string briefName, string definition)
        {
            Lang = lang;
            Name = name;
            BriefName = briefName;
            Definition = definition;
        }

        public string Lang { get; set; }
        public string Name { get; set; }
        public string BriefName { get; set; }
        public string PluralName { get; set; }
        public string Definition { get; set; }
        public string Format { get; set; }
    }
}
