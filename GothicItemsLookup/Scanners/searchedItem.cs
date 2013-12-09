using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using GothicItemsLookup.streamReaders;
namespace GothicItemsLookup.Scanners
{
    class itemFields
    {
        private Dictionary<string, string> _fields;
        public string this[string idx]
        {
            get { if (_fields.ContainsKey(idx)) return _fields[idx]; return ""; }
            set { if (!_fields.ContainsKey(idx)) _fields[idx] = value; }
        }
        public itemFields()
        {
            _fields = new Dictionary<string, string>();
        }
    }
    /// <summary>
    /// After passing ref to stream and constructing object
    /// it will parse next definition of item in file, exctracting instance, some fields and stuff
    /// TODO: Regex'es coudl be better written, use groups!
    /// </summary>
    class searchedItem : IComparable<searchedItem>
    {
        static public string INST_INCORRECT = "~!INCORRECT";
        static private string[] interesingFields = new string[] { "name", "value" };

        public string instance { get; private set; }
        public itemFields fields { get; private set; }
        //public Tuple<uint, uint> blockOfCode { get; private set; }
        public string srcFilePath { get; private set; }

        // instanceFilter there is actually regex filter
        public searchedItem(ref scriptStreamReader stream, stringFilter instFilter)
        {
            srcFilePath = stream.filePath;
            fields = new itemFields();
            string firstLine = _seekToInstance(ref stream);
            uint firstLineNr = stream.currentLine;
            if (_extractInstance(firstLine))
            {
                if (instFilter == stringFilter.FILTER_NONE || !Regex.IsMatch(instance, instFilter.regexPattern, instFilter.options))
                {
                    instance = INST_INCORRECT; return;
                }
            }
            else { instance = INST_INCORRECT; return; }
            // There I can read attribs & stuff
            _collectFieldsTillEndOfDeclarationBlock(ref stream);
            //blockOfCode = new Tuple<uint, uint>(firstLineNr, stream.currentLine);
        }
        public override string ToString()
        {
            const int maxL = 20;
            if (instance.Length < maxL)
                return instance;
            else
                return instance.Insert(maxL - 3, "...").Substring(0, maxL);
        }

        private void _collectFieldsTillEndOfDeclarationBlock(ref scriptStreamReader stream)
        {
            while (!stream.EndOfStream)
            {
                string line = stream.ReadLine();
                if (line.IndexOf("};") != -1) return;
                else if (Regex.IsMatch(line, "\\w.*?=.*?;")) _extractField(line);
            }
        }

        private void _extractField(string line)
        {
            string field = line.Substring(0, line.IndexOf("="));
            field = field.Trim().ToLower();
            if (field[0] != interesingFields[0][0] && field[0] != interesingFields[1][0]) return;
            foreach (string pat in interesingFields)
                if (field == pat) fields[field] = _extractValue(line);
        }

        private string _extractValue(string line)
        {
            try
            {
                int start = line.IndexOf("=") + 1;
                string val = line.Substring(start, line.IndexOf(";") - start);
                return val;
            }
            catch (Exception e) { return "ERROR"; }
        }

        // Returns TRUE if there was actual instance pattern matching line
        private bool _extractInstance(string firstLine)
        {
            bool instanceValid = true;
            instance = Regex.Replace(firstLine, "INSTANCE", "", RegexOptions.IgnoreCase);
            if (instance == firstLine) instanceValid = false;

            firstLine = Regex.Replace(instance, "\\([\\S]*?\\)", "", RegexOptions.IgnoreCase);
            if (instance == firstLine) instanceValid = false;


            instance = firstLine.Trim();
            return instanceValid;
        }
        private string _seekToInstance(ref scriptStreamReader stream)
        {
            string line = stream.ReadLine();
            while (!Regex.IsMatch(line, "INSTANCE.*? [\\w]+.*?\\([\\S]*?\\)", RegexOptions.IgnoreCase))
            {
                if (stream.EndOfStream) break;
                line = stream.ReadLine();
            }
            return line;
        }

        public int CompareTo(searchedItem other)
        {
            return this.instance.CompareTo(other.instance);
        }
    }
}
