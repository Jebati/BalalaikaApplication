using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary.Writers
{
    class StdWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
