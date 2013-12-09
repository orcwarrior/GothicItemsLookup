using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GothicItemsLookup
{
    class stringFilter
    {
        static public stringFilter FILTER_NONE = new stringFilter("","",RegexOptions.None);
        public RegexOptions options { get; private set; }
        private string _wildCardPattern;
        public string wildCardPattern {
            get { if (_wildCardPattern == "")
                    return Utils.RegexToWildcard(_regexPattern);
            return _wildCardPattern; } }

        private string _regexPattern;
        public string regexPattern { 
            get { if (_regexPattern == "") 
                return Utils.WildcardToRegex(_wildCardPattern); 
            return _regexPattern; } }
            
        public stringFilter(string asRegex,string asWildcard,RegexOptions opt)
        {
            _wildCardPattern = asWildcard;
            _regexPattern = asRegex;
            this.options = opt;
        }
    }
}
