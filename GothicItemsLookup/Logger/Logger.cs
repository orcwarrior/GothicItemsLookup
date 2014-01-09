using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;

namespace LogSys
{
    enum eDebugMsgLvl { INFO=1, WARN=2, ERROR=3, INCORRECT=4 }
    class WrongMethodFinalStateExcepton : Exception { }
    class StreamNotClosedProperlyException : Exception { }
    class DebugMessageLevelFilterIncorrectException : Exception { }

    class Logger : IDisposable
    {
        public eDebugMsgLvl logLevel { get; set; }
        private const int _maxMsgInBuffer = 5;
        private StreamWriter _outStream;
        private Queue<LogMsg> _msgQueue;

        static public Logger instance{get; private set;}
        static public bool autoAddMsgToLog { get; private set; }
        private Logger(StreamWriter stream)
        {
            _outStream = stream;
            _msgQueue = new Queue<LogMsg>();
            logLevel = eDebugMsgLvl.INCORRECT;
        }
        static public void Initialize(StreamWriter stream, bool autoAddCreatedMessages = false,eDebugMsgLvl debugLvl = eDebugMsgLvl.ERROR)
        {
            if (instance != null) return;
            autoAddMsgToLog = autoAddCreatedMessages;
            lock(new object())
            {
                if(instance==null)
                {
                    lock(new object())
                    { 
                        instance = new Logger(stream);
                        instance.logLevel = debugLvl;
                    }
                }
            }

        }
        static public void addLogMsg(LogMsg msg)
        {
            if (instance==null) return;
            instance.addMsg(msg);
        }
        public void addMsg(LogMsg msg)
        {
            if (!_filterMsg(msg)) return;
            _msgQueue.Enqueue(msg);
            if (_msgQueue.Count > _maxMsgInBuffer)
                _pushMsgQueueToStream();
        }

        private bool _filterMsg(LogMsg msg)
        {
            return logLevel <= msg.level;
        }


        // Private 
        private void _pushMsgQueueToStream()
        {
            _outStream.Flush();
            while(_msgQueue.Count>0)
                _outStream.WriteLine(_msgQueue.Dequeue());
        }
        private bool _streamIsValid()
        {
            return (_outStream != null && _outStream.BaseStream != null && _outStream.BaseStream.CanWrite);
        }

        public void Dispose()
        {
            _pushMsgQueueToStream();
            _outStream.Close();
        }
    }
}
