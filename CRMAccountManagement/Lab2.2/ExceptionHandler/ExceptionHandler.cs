using Lab2._2.User;
using System;

namespace Lab2._2.ExceptionHandler
{
    public class ExceptionHandler
    {
        public static void AddUnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                Console.Error.WriteLine(((Exception)e.ExceptionObject).Message.ToString());
                Console.ReadKey();
            };
        }
    }
}
