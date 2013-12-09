using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GothicItemsLookup.Scanners.Utils
{
    enum eBlockType { INSTANCE, FUNC, COND, C_INFO }
    class scriptBlock
    {
        public eBlockType blockType { get; private set; }
        public string additionalInfo { get; private set; } // instance,func name, if condition, etc.

        protected scriptBlock(eBlockType type, string infos)
        { blockType = type; additionalInfo = infos; }

        static public scriptBlock tryToCreate(string line, bool condStatementsOnly)
        {
            scriptBlock bl;
            Match m;
            //IF/ELSE IF/ELSE => Conditional
            if (Regex.IsMatch(line, "(if)|(else.*?if)|(else)"))
            {
                m = Regex.Match(line, "(\\(.*\\))"); // <- finds condition statement
                bl = new scriptBlock(eBlockType.COND, m.Value);
                return bl;
            } if (condStatementsOnly) return null;

            // FUNC BLOCK:
            m = Regex.Match(line, "FUNC.*?[\\w]+.*?([\\w]+).*?\\(.*?\\)", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                bl = new scriptBlock(eBlockType.FUNC, m.Groups[1].Value);
                return bl;
            }
            //INSTANCE (NPC or C_INFO) /we don't care bout others
            m = Regex.Match(line, "instance.*?([\\w]+).*?\\((.*?)\\)", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                if (m.Groups[2].Value.Equals("C_INFO"))
                {
                    bl = new scriptBlock(eBlockType.C_INFO, m.Groups[1].Value);
                    return bl;
                }
                else if (!m.Groups[2].Value.Equals("C_ITEM"))
                {
                    bl = new scriptBlock(eBlockType.INSTANCE, m.Groups[1].Value);
                    return bl;
                }
            }
            return null; //<-nothing matched
        }
    }
}
