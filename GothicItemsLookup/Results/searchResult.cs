using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GothicItemsLookup.Scanners;
using System.Windows.Forms;

namespace GothicItemsLookup.Results
{
    public class SearchResultSource
    {
        public string file{get; private set;}
        public int line{get; private set;}
        public string codeLine{get; private set;}

        public SearchResultSource(string f,int l,string cLine)
        { file=f; line=l; codeLine=cLine;}
    }
    abstract public class searchResult
    {
        public string instance { get; private set; } // Quite big redundancy but it's need for MoreInfos method.
        public findResultType type { get; protected set; }
        public SearchResultSource src {get; protected set;}
        public int amount { get; protected set; }
        public System.Drawing.Color myColor { get; protected set; }

        public searchResult(string inst,SearchResultSource src, int iAmnt,findResultType typ)
        { instance = inst; this.src = src; amount = iAmnt; type = typ; }

        public abstract Form MoreInfos();
    }
    class scriptSearchResult : searchResult
    {
        private string self;
        private string other;
        private string conds;

        public scriptSearchResult(string inst,SearchResultSource src, int amnt, findResultType fndResTyp, string slf, string oth, string conds)
        : base(inst,src,amnt,fndResTyp)
        {
            this.self = slf;
            this.other = oth;
            this.conds = conds;
            myColor = _determineMyColor();
        }
        public override string ToString()
        {
            string res = "";
            res += "[" + amount + "]";
            res += type.getName();
            switch (type)
            {
                case findResultType.NPC:
                case findResultType.TRADER: res += ": " + self; break;

                case findResultType.MISSION:
                case findResultType.OTHER: res += ": " + self + ((other!=null)?" => " + other :""); break;
            }
            string filename = GothicItemsLookup.Utils.ExctractFilename(src.file);
            res += ", w: "+filename+":"+src.line;
            return res;
        }
        public override Form MoreInfos()
        {
            List<string> parameters, values;
            parameters = new List<string>(new string[]
                {"Przedmiot:",
                 "Ilość:",
                 "Typ wyniku:",
                 "self:",
                 "other:",
                 "Warunki:",
                 "Plik:",
                 "Linia:",
                 "Podgląd Lini:"});
            values = new List<string>(new string[]
            {instance,
             amount.ToString(),
             type.getName(),
             self,
             other,
             conds,
             src.file,
             src.line.ToString(),
             src.codeLine
            });
            return new resultMoreInfos_Form(parameters, values);
        }

        private System.Drawing.Color _determineMyColor()
        {
            switch (type)
            {
                case findResultType.NPC: return ColorsConst.resultScripts_Npc;
                case findResultType.MISSION: return ColorsConst.resultScripts_Mission;
                case findResultType.TRADER: return ColorsConst.resultScripts_Trader;
                case findResultType.OTHER: return ColorsConst.resultScripts_Other;
            }
            throw new NotImplementedException("Nie zaimplementowano wszystkich przeksztalcen enum=>color");
        }
        
    }
    class zenSearchResult : searchResult
    {
        public int[] pos { get; private set; }
        public zenSearchResult(string inst,SearchResultSource src, int iAmnt, findResultType typ, int[] p)
            : base(inst,src, iAmnt,typ)
        {
            type = typ;
            pos = p;
            myColor = _determineMyColor();
            
        }

        private System.Drawing.Color _determineMyColor()
        {
            return (type == findResultType.CHEST) ? ColorsConst.resultWorlds_Chest : ColorsConst.resultWorlds_Item;
        }
        public override string ToString()
        {
            string res="";
            res += "[" + amount + "]";
            res += type.getName();
            res += " w pozycji("+pos[0]+","+pos[1]+","+pos[2]+") ";
            string filename = GothicItemsLookup.Utils.ExctractFilename(src.file);
            res += filename;
            return res;
        }

        public override Form MoreInfos()
        {
            List<string> parameters, values;
            parameters = new List<string>(new string[]
                {"Przedmiot:",
                 "Ilość:",
                 "Typ wyniku:",
                 "Pozycja X:",
                 "Pozycja Y:",
                 "Pozycja Z:",
                 "Plik:",
                 "Linia:",
                 "Podgląd Lini:"});
            values = new List<string>(new string[]
            {instance,
             amount.ToString(),
             type.getName(),
             pos[0].ToString(),
             pos[1].ToString(),
             pos[2].ToString(),
             src.file,
             src.line.ToString(),
             src.codeLine
            });
            return new resultMoreInfos_Form(parameters, values);
        }
    }

    static class searchResultFactory
    {
        static public searchResult Create(IItemScaner scanner, scannedItem item)
        {
            if (scanner is zenObjScanner) return _CreatefromZenScanner(scanner as zenObjScanner, item);
            else if (scanner is scriptScanner) return _CreatefromScriptScanner(scanner as scriptScanner, item as scriptScannedItem);
            else return null;
        }
        static private searchResult _CreatefromZenScanner(zenObjScanner scanner, scannedItem item)
        {
            SearchResultSource src = new SearchResultSource(scanner.srcFilePath, -1, "");
            string[] posSplit = scanner.objPosStr.Split(' ');
            // Zeby przyspieszyć troche caly proces, parsujemy pozycje dopiero tutaj, gdy jest pewność
            // ze item ma być dodany. (Dzięki Split, pozbywamy sie cyfr za przecinkiem)
            int[] pos = new int[]{int.Parse(posSplit[0].Split('.')[0]),
                                                                int.Parse(posSplit[1].Split('.')[0]),
                                                                int.Parse(posSplit[2].Split('.')[0])};
            return new zenSearchResult(item.instance,src, item.amount,item.resultType , pos);
        
        }
        static private searchResult _CreatefromScriptScanner(scriptScanner scanner, scriptScannedItem item)
        {
            return new scriptSearchResult(item.instance, item.src, item.amount, item.resultType,item.self,item.other,item.conditions);
        }
    }
}
