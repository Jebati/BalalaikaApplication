using System;
using LoggerLibrary;

namespace BalalaikaApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();

            int a = 1;
            logger.Debug("Дебаг информация, a = " + a);
            int b = 2;
            logger.Error("Ошибка, b = " + b);
            int c = 3;
            logger.Warn("Предупреждение, c = " + c);
            int d = 4;
            logger.Info("Обычная информация, d = " + d);

            Console.Read();
        }
    }
}
