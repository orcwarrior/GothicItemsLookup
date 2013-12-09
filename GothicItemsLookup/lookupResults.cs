using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GothicItemsLookup.Scanners;
using GothicItemsLookup.Results;
namespace GothicItemsLookup
{
    /// <summary>
    /// Klasa przechowująca zbiór wszystkich szukanych przedmiotów
    /// Jak i wystąpień tych przedmiotow w postaci searchResultSummary
    /// </summary>
    class lookupResults : IEnumerable<searchResultSummary>
    {
        // Nie myslę jak tu by ładnie sie dokopać do obecnych resaltów,
        // mysle że to jest w miare eleganckie:
        static public lookupResults self { get; private set; }
        Dictionary<string, searchResultSummary> lookupItems;// instancje,Lista znalezionych wyników

        private lookupResults(ICollection<searchedItem> lookupItems)
        {
            // słownik tworzymy uzywajać jako kluczy ZUPERCASEOWANE instance:
            this.lookupItems = new Dictionary<string, searchResultSummary>();
            foreach (searchedItem it in lookupItems)
            {
                if (!this.lookupItems.ContainsKey(it.instance.ToUpper()))
                {
                    this.lookupItems.Add(it.instance.ToUpper(), new searchResultSummary(it.instance));
                }                
            }
        }
        static public void Create(ICollection<searchedItem> lookupItems)
        {   
            self = new lookupResults(lookupItems);
        }
        /// <summary></summary>
        /// <returns>TRUE -> Chociaż 1 z itemów jest przez nas poszukiwany.</returns>
        public bool testWithLookupItems(IItemScaner obj)
        {
            bool someItemAdded = false;
            foreach (scannedItem itm in obj.getFoundedItems())
            {
                string INSTUPPER = itm.instance.ToUpper();
                if (lookupItems.ContainsKey(INSTUPPER))
                {
                    searchResult res = searchResultFactory.Create(obj, itm);
                    lookupItems[INSTUPPER].addEntry(res);
                    someItemAdded = true;
                }
            }
            return someItemAdded;                
        }

        public IEnumerator<searchResultSummary> GetEnumerator()
        {
            return lookupItems.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return lookupItems.Values.GetEnumerator();
        }
    }
}
