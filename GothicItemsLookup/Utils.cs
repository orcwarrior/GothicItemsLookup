using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup
{
    static class Utils
    {
        public static string WildcardToRegex(string pattern)
        {
            return /*"^" + */System.Text.RegularExpressions.Regex.Escape(pattern).
                               Replace(@"\*", ".*").
                               Replace(@"\?", ".");
        }
        public static string RegexToWildcard(string pattern)
        {
            return System.Text.RegularExpressions.Regex.Escape(pattern).
                               Replace(".*", @"\*").
                               Replace(".", @"\?");
        }
        public static string ExctractPath(string fullFilePath)
        {
            return fullFilePath.Substring(0, fullFilePath.LastIndexOf('\\') + 1);
        }
        public static string ExctractFilename(string fullFilePath)
        {
            return fullFilePath.Substring(fullFilePath.LastIndexOf('\\') + 1);
        }
        public static bool isTraderInstance(string instance)
        {   // To umowne i nadaje sie tylko do CZ, w innych wypadkach najlepiej
            // zwrocic false i to olać:
            return instance.ToUpper().Contains("_CONTAINER");
        }
        /// <summary>
        /// Zwraca ilość wystąpien ciągu w zródłowym stringu.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int CountOccurencesOf(this string src, string pattern)
        {
            return (src.Length - src.Replace(pattern, "").Length) / pattern.Length;
        }
    }
}
