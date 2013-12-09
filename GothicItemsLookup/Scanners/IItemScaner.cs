using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GothicItemsLookup.Results;

namespace GothicItemsLookup.Scanners
{
    /// <summary>
    /// Class representing item founded in code of script or zen file.
    /// </summary>
    class scannedItem
    {
        public SearchResultSource src { get; private set; }
        public string instance { get; private set; }
        public int amount { get; private set; }
        public findResultType resultType { get; private set; }
        public scannedItem(string _instance, int _count, findResultType type,SearchResultSource src)
        {
            instance = _instance; amount = _count; resultType = type; this.src = src;
        }
        public void increaseCount(int cnt)
        {
            if (cnt < 0) throw new ArgumentOutOfRangeException("Count must be positive!");
            amount += cnt;
        }
    }
    class scriptScannedItem : scannedItem
    {
        public string conditions { get; private set; }
        public string self { get; private set; }
        public string other { get; private set; }

        public scriptScannedItem(string _instance, int _count, findResultType type, SearchResultSource src,
            string conds, string slf, string oth)
            : base(_instance, _count, type, src)
        {
            conditions = conds; self = slf; other = oth;
        }
    }
    interface IItemScaner
    {
        IEnumerable<scannedItem> getFoundedItems();
    }
}
