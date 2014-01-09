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
                StreamReader reader = new StreamReader(kvp.Key);
                StreamWriter writer = new StreamWriter(kvp.Key + ".bak",false, reader.CurrentEncoding);
                const int maxBytes = 1024;
                int readCnt = maxBytes;
                char[] buffer = new char[readCnt];
                // while the read method returns bytes
                // keep writing them to the output stream
                int lines = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (kvp.Value.Count > 0 && kvp.Value[0].pos == lines+1)
                    { // there is some find-replace todo now:
                        InstanceReplaceJob job = kvp.Value[0];
                        new LogMsg("Replace-Worker: "+job.oldID+" => "+job.newID+", file: "+Utils.ExctractFilename(kvp.Key)+":"+job.pos, eDebugMsgLvl.INFO);
                        line = line.Replace(job.oldID, job.newID);
                        writer.WriteLine(line);
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
