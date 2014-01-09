using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using GothicItemsLookup.streamReaders;
using GothicItemsLookup.Scanners;
using GothicItemsLookup.Scanners.Utils;
using LogSys;

namespace GothicItemsLookup.parsers
{
    class scriptFilesParser : genericFileParser
    {
        static private string contentFolder;
        static List<string> scriptsToParse;
        static List<itemAddingFunc> lookupFunctions;
        static public void Parse(string gothic_src, List<itemAddingFunc> itemsAddFuncEnum)
        {
            new LogMsg("Game scipts: Start parsing...", eDebugMsgLvl.INFO);
            lookupFunctions = itemsAddFuncEnum;
            contentFolder = gothic_src.Substring(0, gothic_src.LastIndexOf("\\")+1);
            StreamReader gothicSrc = new StreamReader(gothic_src);
            new LogMsg("Game scipts: gothic.src loc: "+gothic_src, eDebugMsgLvl.INFO);
            if (gothicSrc.BaseStream != null)
            {
                callProgressUpdate(-1,0, "Czytanie pliku gothic.src..");
                List<string> lines = new List<string>();
                while (!gothicSrc.EndOfStream)
                    lines.Add(gothicSrc.ReadLine());
                callProgressUpdate(-1, 0.001, "Tworzenie listy skryptów do sprasowania");
                new LogMsg("Game scipts: gothic.src entries: "+lines.Count, eDebugMsgLvl.INFO);

                //Generate full filelist: (replace wildcards)
                scriptsToParse = _generateFullFileList(lines);
                new LogMsg("Game scipts: gothic.src actual files to parse: " + scriptsToParse.Count, eDebugMsgLvl.INFO);
                gothicSrc.Close();
                _LookupForItems(); // Method will done rest of stuff.
            }
        }
        // Generates full list of script files to parse based on lines in gothic.src
        // (including impreting of wildcard pattern - loading multiple files aswell)
        private static List<string> _generateFullFileList(List<string> lines)
        {
            List<string> filesList = new List<string>();
            int counter = 0;
            foreach (string entry in lines)
            {
                string fullPath = contentFolder + entry;
                if (File.Exists(fullPath))
                    filesList.Add(fullPath);
                else // maybe it's some wildcardStuff:
                {
                    string searchPattern = fullPath.Substring(fullPath.LastIndexOf("\\") + 1).Trim();
                    if (searchPattern.Length > 0)
                    {
                        string fullDirectory = fullPath.Substring(0, fullPath.LastIndexOf("\\") + 1);
                        filesList.AddRange(Directory.GetFiles(fullDirectory, searchPattern));
                    }
                }
                counter++;
                if (counter % 8 == 7)
                    callProgressUpdate(-1, ((double)counter / lines.Count) * 0.2, "Tworzenie listy skryptów... " + entry + "..." + filesList.Count);

            }
            filesList = filesList.Distinct().ToList();
            return filesList;
        }

        static private void _LookupForItems()
        {
            orginalScriptsCnt = scriptsToParse.Count;
            // Lookup for items with 4 Threads, each one will be checking
            // one file at once
            Thread[] lookupThreads = new Thread[4];
            new LogMsg("Game scipts: Start lookup for items threads", eDebugMsgLvl.INFO);
            for (int i = 0; i < lookupThreads.Length; i++)
            {
                lookupThreads[i] = new Thread(() => _LookupForItems_Sub());
                lookupThreads[i].Priority = ThreadPriority.Highest;
                lookupThreads[i].Start();
            }
            bool someThreadAlive = true;
            while (someThreadAlive)
            {
                someThreadAlive = false;
                int aliveCnt = 0;
                foreach (Thread t in lookupThreads)
                {
                    someThreadAlive |= t.IsAlive;
                    aliveCnt++;
                }
                Thread.Sleep(250);
                new LogMsg("Game scipts: Threads check, still alive: " + aliveCnt+"/"+lookupThreads.Length, eDebugMsgLvl.INFO);
            }

        }
        static private Object lookupListLock = new Object();
        static private int processedFilesCnt,orginalScriptsCnt;
        static private void _LookupForItems_Sub()
        {
            new LogMsg("Game scipts: Start lookup-sub-thread...", eDebugMsgLvl.INFO);
            while(scriptsToParse.Count > 0)
            {
                string scriptPath;
                scriptStreamReader script;
                // critical section:
                lock(lookupListLock)
                {
                    scriptPath = scriptsToParse[0];
                    scriptsToParse.RemoveAt(0); // <- remove loaded fileName from list, so it willn't be readed further
                }
                if(scriptPath == null) break; // <- sth went wrong with threads, there is nothing more to parse
                script = new scriptStreamReader(scriptPath);
                new scriptScanner(ref script, lookupFunctions);
                // Update progress:
                processedFilesCnt++;
                if (processedFilesCnt % 7 == 0)
                    callProgressUpdate(-1, 0.2 + 0.8 * ((double)processedFilesCnt) / orginalScriptsCnt,
                        "...Parsowanie: " + scriptPath);
            }
        }
    }
}
