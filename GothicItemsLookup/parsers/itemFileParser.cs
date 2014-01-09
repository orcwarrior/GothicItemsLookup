using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GothicItemsLookup.streamReaders;
using GothicItemsLookup.Scanners;
using LogSys;

namespace GothicItemsLookup.parsers
{
    // prawie jak singleton :)
    // nazwa klasy z dupy, nie wymysle lepszej
    class itemFileParser : genericFileParser
    {
        static private stringFilter _filter;
        static private double[] progress;
        static public ICollection<searchedItem> Parse(ICollection<string> filePaths, stringFilter filter)
        {

            new LogMsg("Items-Scipt-Extract: \titemFileParser started!", eDebugMsgLvl.INFO);
            progress = new double[]{ 0, (double)filePaths.Count };
            List<searchedItem> list = new List<searchedItem>();
            _filter = filter; // convert wildcard filter to regex and store it
            foreach (string file in filePaths)
            {
                new LogMsg("Items-Scipt-Extract: \tParsing file: "+Utils.ExctractFilename(file), eDebugMsgLvl.INFO);
                callProgressUpdate(-1, progress[0]++ / progress[1], "Parsuje: " + file);
                curFile = file;
                list.AddRange(_parseFile(file));
            }
            callProgressUpdate(-1, -1, "Gotowe");
            jobDone = true;
            return list;
        }

        static public ICollection<searchedItem> _parseFile(string path)
        {
            List<searchedItem> list = new List<searchedItem>();
            scriptStreamReader sr = new scriptStreamReader(path);
            while (!sr.EndOfStream)
            {
                // Sam "ogarnie" sobie instance i przesunie stream az poza swój blok kodu
                searchedItem itm = new searchedItem(ref sr,_filter);
                if (itm.instance != searchedItem.INST_INCORRECT)
                    list.Add(itm); 
            }
            return list;
        }

        public static System.ComponentModel.BackgroundWorker worker { get; set; }
    }
}
