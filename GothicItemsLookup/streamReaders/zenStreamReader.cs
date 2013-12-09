using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup.streamReaders
{
    class zenStreamReader :genericStreamReader
    {
        public string lastLine { get; private set; }
        public zenStreamReader(string path)
            :base(path)
        {
        }
        public override string ReadLine()
        {
            return lastLine = base.ReadLine();
        }
    }
}
