using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup.streamReaders
{
    // Moja rozszerzająca klasa stream readera, przyda sie troche
    // pierdół, nie wiem na ile to poprawne, ale postanawiam przechowywać
    // to w samym streamie
    abstract class genericStreamReader : System.IO.StreamReader
    {
        public string filePath { get; private set; }

        public genericStreamReader(string path)
            :base(path,Encoding.Default)
        {
            filePath = path;
        }

        // Disabled methods:
        public override int  Read()
        { throw new NotImplementedException();}
        public override int  Read(char[] buffer, int index, int count)
        { throw new NotImplementedException();}
        public override int  ReadBlock(char[] buffer, int index, int count)
        { throw new NotImplementedException();}
        public override string  ReadToEnd()
        { throw new NotImplementedException();}
    }
}
