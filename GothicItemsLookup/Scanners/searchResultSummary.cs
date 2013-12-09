using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup.Results
{
    // Jeden obiekt klasy dla jednej instancji, będzie
    // zawieralo liste znalezionych wyników we wszystkich plikach
    class searchResultSummary : IEnumerable<searchResult>
    {
        public string instance { get; private set; }
        public int amount { get; private set; }
        private List<searchResult> entries;

        public searchResultSummary(string inst)
        {
            entries = new List<searchResult>();
            instance = inst;
        }
        public void addEntry(searchResult res)
        { entries.Add(res); amount += res.amount; }


        public IEnumerator<searchResult> GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return entries.GetEnumerator();
        }
        public override string ToString()
        {
            return "[" + amount + "] " + instance;
        }
    }
}
