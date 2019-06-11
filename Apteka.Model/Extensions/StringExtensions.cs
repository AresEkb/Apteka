﻿using System;
using System.Linq;

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
    }
}
