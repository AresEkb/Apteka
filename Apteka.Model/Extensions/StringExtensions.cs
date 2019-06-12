using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Apteka.Model.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            return String.IsNullOrWhiteSpace(input)
                ? input
                : input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string FirstCharToLower(this string input)
        {
            return String.IsNullOrWhiteSpace(input)
                ? input
                : input.First().ToString().ToLower() + input.Substring(1);
        }

        // https://stackoverflow.com/a/9545731/632199
        public static long KnuthHash(this string str)
        {
            ulong hashedValue = 3074457345618258791ul;
            for (int i = 0; i < str.Length; i++)
            {
                hashedValue += str[i];
                hashedValue *= 3074457345618258799ul;
            }
            return unchecked((long)hashedValue + long.MinValue);
        }

        public static bool TryMatchMeasure(this string str, out string prefix, out decimal measure, out string unit)
        {
            var m = Regex.Match(str, @"^(.*?[^+])\s+([0-9]+([.,][0-9]+)?)\s*([^0-9(),]{1,20})?$");
            if (m.Success)
            {
                if (decimal.TryParse(m.Groups[2].Value.Replace(',', '.'), out measure))
                {
                    prefix = m.Groups[1].Value.TrimEnd(',');
                    unit = m.Groups[4].Value;
                    return true;
                }
            }
            prefix = null;
            measure = 0;
            unit = null;
            return false;
        }
    }
}
