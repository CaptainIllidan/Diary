using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diary.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return toCheck==null
                ? true
                : source?.IndexOf(toCheck, comp) >= 0;
        }
        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return Contains(source, toCheck, StringComparison.OrdinalIgnoreCase);
        }
    }
}