using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakeAwayTech.Classes
{
    public static class Extensions
    {
        public static string AppendWithSpace(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return " " + input;
        }
        public static string AppendWithHyphen(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return "-" + input;
        }
        public static string AppendWithAnd(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return " AND" + input;
        }
    }
}