using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup.Scanners.Utils
{
    enum eItemFunc_Param { srcNpc=0, dstNpc=1, itemID=2, count=3, wp=4, irrelevant}
    // Extension of enum, enum => string (user friendly name)
    internal static class itemAddingFunc_Param_Extension
    {
        internal static string getName(this eItemFunc_Param e)
        {
            switch (e)
            {
                case eItemFunc_Param.srcNpc: return "$SRC";
                case eItemFunc_Param.dstNpc: return "$DST";
                case eItemFunc_Param.itemID: return "$ITEM";
                case eItemFunc_Param.count: return "$COUNT";
                case eItemFunc_Param.wp: return "$WP";
                case eItemFunc_Param.irrelevant: return "???";
            }
            throw new NotImplementedException("enum extension getName(), not fully implemented!");
        }
    }
    // Definiuje pojedyncza funkcje sluzaca do dodawania itemow, oraz
    // 
    class itemAddingFunc
    {
        public string name { get; private set; }
        public eItemFunc_Param[] parameters;
        public itemAddingFunc(string _name, eItemFunc_Param[] _params)
        { name = _name; parameters = _params; }
        public override string ToString()
        {
            string res = name + "(";
            foreach (eItemFunc_Param p in parameters)
                res += p.getName() + ",";
            res = res.Substring(0, res.Length - 1) + ");";
            return res;
        }
    }
    internal static class itemAddingFunc_Extension
    {
        /// <summary>
        /// Return array of objects sorted same way as eItemFunc_Param enumeration
        /// </summary>
        /// <param name="paramsStr">params String, like: self,ItMiNugget</param>
        /// <returns></returns>
        internal static object[] exctractParameters(this itemAddingFunc func,string paramsStr)
        {
            int paramsMax = (int)eItemFunc_Param.irrelevant;
            int[] mapper = new int[paramsMax];
            for (int i = 0; i < func.parameters.Length; i++)
            {
                mapper[i] = (int)func.parameters[i];
            }
            string[] paramsUnprocessed = paramsStr.Split(',');
            object[] resParams = new object[paramsMax];
            resParams[(int)eItemFunc_Param.count] = 1;
            //resParams.Select(p => p = ""); // declare all as empty string
            for (int i = 0; i < paramsUnprocessed.Length; i++)
            {
                if (paramsUnprocessed.Length >= i)
                {
                    if (mapper[i] == (int)eItemFunc_Param.count) // convert item amount to int:
                        try
                        {
                            resParams[mapper[i]] = int.Parse(paramsUnprocessed[i]);
                        }
                        catch (Exception e)
                        {
                            resParams[mapper[i]] = paramsUnprocessed[i];
                        }
                    else
                        resParams[mapper[i]] = paramsUnprocessed[i];
                }
            }
            return resParams;
        }
    }
}
