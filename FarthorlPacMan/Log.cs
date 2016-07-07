using System;
using System.IO;

namespace FarthorlPacMan
{
    public class Log
    {
        private static string logFile= @"DataFiles\Logs\log.txt";
        private static int count = 1;
        public static void LogText(string text)
        {
            using (var fileLog = new StreamWriter(logFile, true))
            {
                string recordLine =count + ". " + DateTime.Now + ":" + text;
                fileLog.WriteLine(recordLine);
                count++;
            }
        }
    }
}