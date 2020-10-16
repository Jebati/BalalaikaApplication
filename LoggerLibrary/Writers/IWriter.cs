using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerLibrary.Writers
{
    interface IWriter
    {
        void Write(string message);
    }
}
