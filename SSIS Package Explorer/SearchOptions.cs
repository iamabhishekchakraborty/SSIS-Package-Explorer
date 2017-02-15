using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSIS_Package_Reader
{
    public static class SearchOptions
    {
        public static bool IsCaseSensitive;
        public static bool MatchSubstring = true;
        public static bool MatchWholeWord;
        public static bool MatchRegularExpression;
        public static ExpressionTypes ExpressionType;

        public enum ExpressionTypes
        { 
            None = 1,
            RegularExpression = 2,
            WildCard = 3
        }
    }
}
