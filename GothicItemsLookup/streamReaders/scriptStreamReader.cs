using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GothicItemsLookup.streamReaders
{
    class scriptStreamReader : genericStreamReader
    {
        public bool streamActuallyInBlockComment { get; private set; }
        //public string currentMethod { get; private set; }
        public uint currentLine { get; private set; }
        public long currentByte { get; private set; }
        private List<string> _lastLines;

        public scriptStreamReader(string path)
            :base(path)
        {
            _lastLines = new List<string>(6);
        }
        public override string ReadLine()
        {
            
            string line = base.ReadLine();
            currentLine++; currentByte += base.CurrentEncoding.GetByteCount(line);
            _updateLastLines(line); // robimy to przed usunieciem komentarza (dla podglądu np. dialogów)
            line = _stripLineComments(line);
            bool newBlockCommentState = _checkIfLineStartBlockComment(line);
            if (newBlockCommentState && !streamActuallyInBlockComment)
            { // poczatek komentarza blokowego:
                line = line.Substring(0, line.IndexOf("/*"));
            }
            else if (!newBlockCommentState && streamActuallyInBlockComment)
            {//koniec komentarza blokowego:
                line = line.Substring(line.LastIndexOf("*/"));
            }
            else if (streamActuallyInBlockComment) line = ""; // ciagle w komentarzu blokowym
            streamActuallyInBlockComment = newBlockCommentState;
            return line;
        }
        public string getLastLines(int idx)
        {
            return _lastLines[idx];
        }

        // Private methods
        private bool _checkIfLineStartBlockComment(string line)
        {
         	int lastBlockCommStart = line.LastIndexOf("/*");
            int lastBlockCommEnd = line.LastIndexOf("*/");
            if(lastBlockCommStart > lastBlockCommEnd) return true;
            else if(lastBlockCommStart == lastBlockCommEnd) return streamActuallyInBlockComment;
            else return false;
        }
        private void _updateLastLines(string line)
        {                           
            _lastLines.Insert(0,line);
            if (_lastLines.Count > 5) _lastLines.RemoveAt(5);
        }
        private string _stripLineComments(string l)
        {
            return Regex.Replace(l,"//.*",""); // usunie komentarze takie jak ten :)
        }

        
    }
}
