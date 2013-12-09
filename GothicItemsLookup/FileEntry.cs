using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup
{
    class FileEntry
    {
        public string fullpath { get; private set; }
        private string _filename;
        public string filename { get { return _filename; } }

        public FileEntry(string path)
        {
            fullpath = path;
            _filename = fullpath.Substring(fullpath.LastIndexOf("\\") + 1);
        }
        public override string ToString()
        {
            return filename;
        }
    }
}
