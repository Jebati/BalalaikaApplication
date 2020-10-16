using LoggerLibrary.Configurator;
using LoggerLibrary.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary
{
    using Configure = Configurator.Configurator;
    using Writer = FileWriter.Writer;
    public class Logger : ILogger
    {

        FileWriter _fileWriter;
        StdWriter _stdWriter;

        Writer _writers;
        Writer _debugWriters;
        Writer _errorWriters;
        Writer _warnWriters;
        Writer _infoWriters;

        private Config _config;

        public Logger()
        {
            _config = GetConfig();
            AddHandlers();
        }

        private void AddHandlers()
        {
            if (_config.LogToStd)
            {
                _stdWriter = new StdWriter();
                _writers += _stdWriter.Write;
            }
            if (_config.LogToFile)
            {
                _fileWriter = new FileWriter();
                _writers += _fileWriter.Write;


                _fileWriter.AddWriter(ref _fileWriter.streamAnyLog, _fileWriter.WriteAny, ref _fileWriter.writer, _config.Path);

                if (_config.SplitLevels)
                {
                    _fileWriter.AddWriter(ref _fileWriter.streamDebugLog, _fileWriter.WriteDebug, ref _fileWriter.debugWriter, _config.PathsLogLevels.Debug);
                    _fileWriter.AddWriter(ref _fileWriter.streamErrorLog, _fileWriter.WriteError, ref _fileWriter.errorWriter, _config.PathsLogLevels.Error);
                    _fileWriter.AddWriter(ref _fileWriter.streamWarnLog, _fileWriter.WriteWarn, ref _fileWriter.warnWriter, _config.PathsLogLevels.Warn);
                    _fileWriter.AddWriter(ref _fileWriter.streamInfoLog, _fileWriter.WriteInfo, ref _fileWriter.infoWriter, _config.PathsLogLevels.Info);

                    if (_config.LogLevel <= 3) _infoWriters += _fileWriter.infoWriter;
                    if (_config.LogLevel <= 2) _warnWriters += _fileWriter.warnWriter;
                    if (_config.LogLevel <= 1) _errorWriters += _fileWriter.errorWriter;
                    if (_config.LogLevel == 0) _debugWriters += _fileWriter.debugWriter;
                }
            }

            if(_config.LogLevel <= 3) _infoWriters += _writers;
            if (_config.LogLevel <= 2) _warnWriters += _writers;
            if (_config.LogLevel <= 1) _errorWriters += _writers;
            if (_config.LogLevel == 0) _debugWriters += _writers;
        }


        private Config GetConfig()
        {
            return Configure.GetConfigAsync().Result;
        }

        public async void Debug(string message, string prefix = "DEBUG", LogLevel level = LogLevel.Debug)
        {
            await WriteAsync(_debugWriters, GetPrefix(prefix) + message);
        }

        public async void Error(string message, string prefix = "ERROR", LogLevel level = LogLevel.Error)
        {
            await WriteAsync(_errorWriters, GetPrefix(prefix) + message);
        }

        public async void Warn(string message, string prefix = "WARN", LogLevel level = LogLevel.Warn)
        {
            await WriteAsync(_warnWriters, GetPrefix(prefix) + message);
        }

        public async void Info(string message, string prefix = "INFO", LogLevel level = LogLevel.Info)
        {
            await WriteAsync(_infoWriters, GetPrefix(prefix) + message);
        }

        private Task WriteAsync(Writer write, string message)
        {
            return Task.Run(() => write?.Invoke(message));
        }

        private string GetPrefix(string prefix)
        {
            return $"[{prefix}{(_config.TimeStamp ? " " + DateTime.Now.ToString("hh:mm:ss") : "")}]: ";
        }
    }
}
