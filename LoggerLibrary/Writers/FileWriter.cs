using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary.Writers
{
    public class FileWriter : IWriter
    {
        FileWriterQueue _fileWriterQueue = new FileWriterQueue();

        public delegate void Writer(string message);
        public Writer writer;

        public Writer debugWriter;
        public Writer errorWriter;
        public Writer warnWriter;
        public Writer infoWriter;

        public StreamWriter streamAnyLog;
        public StreamWriter streamDebugLog;
        public StreamWriter streamErrorLog;
        public StreamWriter streamWarnLog;
        public StreamWriter streamInfoLog;

        public void Write(string message)
        {
            writer?.Invoke(message);
        }
        public void WriteAny(string message)
        {
            WriteToFile(streamAnyLog, message);
        }
        public void WriteDebug(string message)
        {
            WriteToFile(streamDebugLog, message);
        }
        public void WriteError(string message)
        {
            WriteToFile(streamErrorLog, message);
        }
        public void WriteWarn(string message)
        {
            WriteToFile(streamWarnLog, message);
        }
        public void WriteInfo(string message)
        {
            WriteToFile(streamInfoLog, message);
        }
        private void WriteToFile(StreamWriter stream, string message)
        {
            _fileWriterQueue.Add(
                new InQueue { 
                    stream = stream, 
                    text = message 
                }
            );
        }

        public void AddWriter(ref StreamWriter stream, Writer writer, ref Writer writerLevel, string path)
        {
            stream = new StreamWriter(path, true);
            writerLevel += writer;
        }
    }
}
