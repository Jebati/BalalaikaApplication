using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLibrary.Configurator
{
    public class Config
    {
        public bool LogToStd { get; set; } = false;
        public bool LogToFile { get; set; } = false;
        public bool SplitLevels { get; set; } = false;
        public bool TimeStamp { get; set; } = false;
        public byte LogLevel { get; set; } = 0;
        public string Path { get; set; } = String.Empty;
        public PathsLogLevels PathsLogLevels { get; set; } = new PathsLogLevels();
    }

    public class PathsLogLevels
    {
        public string Debug { get; set; } = String.Empty;
        public string Error { get; set; } = String.Empty;
        public string Warn { get; set; } = String.Empty;
        public string Info { get; set; } = String.Empty;
    }
}
