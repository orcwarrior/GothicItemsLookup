using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicItemsLookup.parsers
{
    public delegate void updateProgress(double progrPartial, double progrWhole, string statusMSG);
    abstract class genericFileParser
    {
        static public string curFile { get; protected set; }
        static public updateProgress progressUpdate;
        static public bool jobDone { get; protected set; }

        static protected void callProgressUpdate(double progressPart,double progressWhole, string msg)
        { if(progressUpdate!=null) progressUpdate(progressPart,progressWhole,msg); }
    }
}
