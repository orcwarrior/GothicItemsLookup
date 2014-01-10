using LogSys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GothicItemsLookup.Results
{
    class InstanceReplaceWorker
    {
        Dictionary<string, List<InstanceReplaceJob>> instanceReplaceQuery;
        public InstanceReplaceWorker() { instanceReplaceQuery = new Dictionary<string, List<InstanceReplaceJob>>(); }

        public void addReplaceJob(string file, InstanceReplaceJob job)
        {
            if (!instanceReplaceQuery.ContainsKey(file))
                instanceReplaceQuery.Add(file, new List<InstanceReplaceJob>());
            instanceReplaceQuery[file].Add(job);
        }
        public void doJobs()
        {
            foreach (KeyValuePair<string, List<InstanceReplaceJob>> kvp in instanceReplaceQuery)
            {

                //sort list so replacements will be sorted by position in file
                kvp.Value.Sort();
                //do actual job: using (FileStream stream = File.OpenRead("C:\\file1.txt"))
                StreamReader reader = new StreamReader(kvp.Key,Encoding.Default,true);
                // FIX: Produced files were in UTF8 format (dunno why) when input script files
                // was 1252 (ANSI) so they're probably System-Default coded, anyways Gothic parser
                // don't like UTF8 encoding (error when parsing)
                StreamWriter writer = new StreamWriter(kvp.Key + ".bak",false, Encoding.Default);
                const int maxBytes = 1024;
                int readCnt = maxBytes;
                char[] buffer = new char[readCnt];
                // while the read method returns bytes
                // keep writing them to the output stream
                new LogMsg("Replace-Worker: Changing instance in file: "+kvp.Key, eDebugMsgLvl.INFO);
                new LogMsg("Replace-Worker: reader encoding: " + reader.CurrentEncoding.EncodingName + ", writer: " + writer.Encoding.EncodingName, eDebugMsgLvl.INFO);
                int lines = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string convertedLine = line;
                    if (!reader.CurrentEncoding.Equals(writer.Encoding))
                    {
                        byte[] lineRead = reader.CurrentEncoding.GetBytes(line);
                        byte[] lineANSI = Encoding.Convert(reader.CurrentEncoding, writer.Encoding, lineRead);
                        convertedLine = writer.Encoding.GetString(lineANSI);
                    }
                    //string convertedLine = line;
                    if (kvp.Value.Count > 0 && kvp.Value[0].pos == lines+1)
                    { // there is some find-replace todo now:
                        InstanceReplaceJob job = kvp.Value[0];
                        new LogMsg("Replace-Worker: "+job.oldID+" => "+job.newID+", file: "+Utils.ExctractFilename(kvp.Key)+":"+job.pos, eDebugMsgLvl.INFO);
                        convertedLine = convertedLine.Replace(job.oldID, job.newID);
                        writer.WriteLine(convertedLine);
                        kvp.Value.RemoveAt(0); // <-job done remove
                    }
                    else
                        writer.WriteLine(line);
                    lines++;
                }
                reader.Close();
                writer.Close();
                File.Delete(kvp.Key);
                File.Move(kvp.Key + ".bak", kvp.Key);
                // TODO: remove/rename files
            }
        }
    }


    class InstanceReplaceJob : IComparable<InstanceReplaceJob>
    {
        public int pos { get; private set; }
        public string oldID { get; private set; }
        public string newID { get; private set; }
        public InstanceReplaceJob(int _pos, string _oldID, string _newID)
        { pos = _pos; oldID = _oldID; newID = _newID; }

        // Compare jobs by positions in file (needed for sort of Jobs List)
        public int CompareTo(InstanceReplaceJob other)
        {
            return pos.CompareTo(other.pos);
        }
    }
}
