using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LogSys
{
    class LogMsg
    {
        public eDebugMsgLvl level { get; protected set; }
        public DateTime date { get; protected set; }
        private string msg;
        private string filename;
        private int line, column;
        public LogMsg(string txt, eDebugMsgLvl level)
        {
            this.level = level;
            date = DateTime.Now;
            msg = txt;
            _exctractFileAndLineNr();
            if (Logger.autoAddMsgToLog && Logger.instance != null)
                Logger.addLogMsg(this);
        }

        private void _exctractFileAndLineNr()
        {
            StackTrace stackTrace = new StackTrace(true);           // get call stack with filenames infos too
            if (stackTrace != null)
            {
                StackFrame underlyingStackFrame = stackTrace.GetFrame(StackTrace.METHODS_TO_SKIP + 2);
                filename = underlyingStackFrame.GetFileName();
                if (filename != null)
                    filename = filename.Substring(filename.LastIndexOf('\\') + 1);
                else filename = "unknow";
                line = underlyingStackFrame.GetFileLineNumber();
                column = underlyingStackFrame.GetFileColumnNumber();
            }
        }

        public override string ToString()
        {
            return "[" + date.ToLongTimeString() + "]:"+level.ToString()+": " + msg + "  at: "+filename+":"+line+":"+column + "\t Thread: "+System.Threading.Thread.CurrentThread.ManagedThreadId;
        }
    }
}
