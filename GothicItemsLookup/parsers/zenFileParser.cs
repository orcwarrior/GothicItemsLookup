using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GothicItemsLookup.streamReaders;
using System.Text.RegularExpressions;
using GothicItemsLookup.Scanners;
using System.Threading;
using LogSys;

namespace GothicItemsLookup.parsers
{
    class zenFileParser : genericFileParser
    {
        static public long[] progressPart { get; private set; }
        static public long[] progressWhole { get; private set; }
        static public string progressStatus { get; private set; }

        static private bool parsingFileFinished; // <- are parsers finished parsing current zen file?

        static public void Parse(ICollection<string> filePaths)
        {
            new LogMsg("ZEN-Lookup: Start parsing zen files...", eDebugMsgLvl.INFO);
            progressWhole = new long[] { 0, filePaths.Count };
            parsingFileFinished = true;
            foreach (string file in filePaths)
            {
                curFile = file;
                if (_checkIfZenIsValid(file))
                {
                    new LogMsg("ZEN-Lookup: parsing zen: "+Utils.ExctractFilename(file), eDebugMsgLvl.INFO);
                    progressStatus = file.Substring(file.LastIndexOf("\\") + 1);
                    while (!parsingFileFinished) Thread.Sleep(200);
                    callProgressUpdate(0, (double)progressWhole[0] / progressWhole[1], progressStatus);
                    zenFileParser._parseFile(file);
                    progressWhole[0]++;
                }
                else
                {
                    new LogMsg("ZEN-Lookup: Selected zen: "+Utils.ExctractFilename(file)+" is binary!", eDebugMsgLvl.WARN);
                    System.Windows.Forms.MessageBox.Show("Plik: " + file + "\n Jest binarnym plikiem ZEN!\nNależy go zapisac w formacie ASCII!");
                }
            }
            while (!parsingFileFinished) Thread.Sleep(250);
            callProgressUpdate(1, 1, "Przeszukiwanie światow skończone");
            jobDone = true;
        }

        static public void _parseFile(string path)
        {
            new LogMsg("ZEN-Lookup: \tSingle-ZEN parsing started...", eDebugMsgLvl.INFO);
            zenStreamReader sr = new zenStreamReader(path);
            _findVobTreeStartPosition(ref sr);
            new LogMsg("ZEN-Lookup: \tFounded vobTree start: "+sr.BaseStream.Position, eDebugMsgLvl.INFO);
            long strLoc = sr.BaseStream.Position;
            sr.DiscardBufferedData();
            progressPart = new long[] { 1000, sr.BaseStream.Length - strLoc };
            callProgressUpdate(progressPart[0] / progressPart[1], progressWhole[0] / progressWhole[1], "...znaleziono początek Vobtree");        

            parsingFileFinished = false;
            long divPosition = (long)((sr.BaseStream.Length - strLoc) * 1.5 / 5.0) + strLoc;
            new LogMsg("ZEN-Lookup: \tSelected Threads div pos: " + divPosition + " (1.5/5)", eDebugMsgLvl.INFO);
            Thread t1 = new Thread(() => _parseFileStream1(path, strLoc, divPosition));
            Thread t2 = new Thread(() => _parseFileStream2(path, divPosition, sr.BaseStream.Length));

            t1.Start(); t2.Start();

            while (t1.IsAlive || t2.IsAlive) Thread.Sleep(200);
            parsingFileFinished = true;
            sr.Close();

        }

        static private bool _checkIfZenIsValid(string path)
        {
            zenStreamReader sr = new zenStreamReader(path);
            sr.ReadLine(); 
            sr.ReadLine();
            string archTypeLine = sr.ReadLine();
            if (archTypeLine.StartsWith("zCArchiverBinSafe")) return false;
            else return true;
        }
        static zenObjScanner str1Obj,str2Obj;
        static long str2Progress;
        static public void _parseFileStream1(string path, long startLoc, long endLoc)
        {
            zenStreamReader sr = new zenStreamReader(path);
            sr.BaseStream.Seek(startLoc,System.IO.SeekOrigin.Begin);
            int progressUpdateInterval = 20;
            while (sr.BaseStream.Position < endLoc)
            {
                str1Obj = new zenObjScanner(ref sr);
                if (str1Obj.action == zenObjScanner.ZENOBJ_ACTION.BREAK_LOOP)
                { sr.BaseStream.Seek(endLoc,System.IO.SeekOrigin.Begin); break; }
                progressUpdateInterval--;
                if (progressUpdateInterval == -10)
                {
                    progressStatus = path.Substring(path.LastIndexOf("\\") + 1) + " " + str1Obj.definitionLine;
                    if (str2Obj != null) progressStatus += " | " + str2Obj.definitionLine;
                    progressUpdateInterval = 20;
                    progressPart[0] = sr.BaseStream.Position - startLoc + str2Progress;
                    double[] pDone = new double[] { (double)progressPart[0] / progressPart[1], 
                                                    (double)progressWhole[0] / progressWhole[1] };
                    callProgressUpdate(pDone[0], pDone[1], progressStatus);
                    new LogMsg("ZEN-Lookup: \t#1 Current stream pos: " + sr.BaseStream.Position, eDebugMsgLvl.INFO);
                }
            }

        }
        static public void _parseFileStream2(string path, long startLoc, long endLoc)
        {
            zenStreamReader sr = new zenStreamReader(path);
            sr.BaseStream.Seek(startLoc, System.IO.SeekOrigin.Begin);
            while (sr.BaseStream.Position < endLoc)
            {
                str2Obj = new zenObjScanner(ref sr);
                if (str2Obj.action == zenObjScanner.ZENOBJ_ACTION.BREAK_LOOP)
                { str2Progress = endLoc; break; }
                str2Progress = sr.BaseStream.Position - startLoc;
                new LogMsg("ZEN-Lookup: \t#2 Current stream pos: " + sr.BaseStream.Position, eDebugMsgLvl.INFO);
            }

        }

        // By runing binary-search:
        private static void _findVobTreeStartPosition(ref zenStreamReader sr)
        {
            long jumpSize,lastPos = sr.BaseStream.Length / 2;
            jumpSize = lastPos;
            sr.BaseStream.Seek(jumpSize,System.IO.SeekOrigin.Begin);
            while (true)
            {
                sr.DiscardBufferedData();
                sr.ReadLine(); sr.ReadLine(); // Seek może ustawić kursor w zlym pkt. lini, dlatego czytamy 2
                                              // linijki, wtedy druga bedzie pełną linią
                sr.DiscardBufferedData();
                jumpSize = jumpSize / 2 + ((jumpSize % 2==0) ? 1 : 0);
                if (sr.lastLine.StartsWith("\t\t"))
                    lastPos = lastPos - jumpSize;
                else if (sr.lastLine.Equals("\t[VobTree % 0 0]"))
                    break;
                else 
                    lastPos = lastPos + jumpSize;
                sr.BaseStream.Seek(lastPos, System.IO.SeekOrigin.Begin);
            }
        }
        public static System.ComponentModel.BackgroundWorker worker { get; set; }
    }
}