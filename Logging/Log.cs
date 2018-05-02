using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class Log
    {
        public delegate void LogEventHandler(string Msg, int severity);
        public static event LogEventHandler NewMessage;
        public static string LogFileDirectoryPath { get; set; }
        public static int LogFilesToKeep { get; set; }
        public static int MaxLogSize { get; set; }
        public static int LogLevel { get; set; }

        public Log(string logFileDirectoryPath, int logFilesToKeep, int maxLogSize, int logLevel)
        {
            LogFileDirectoryPath = logFileDirectoryPath;
            LogFilesToKeep = logFilesToKeep;
            MaxLogSize = maxLogSize;
            LogLevel = logLevel;
        }

        public enum LogType
        {
            Error = 1,
            Warning = 2,
            Informational = 3,
            Silent = 4
        }

        public void Write(LogType severity, string logFile, string message, int level)
        {
            if (NewMessage != null)
                NewMessage(DateTime.Now.ToString() + " " + message, (int)severity);

            if (!Directory.Exists(LogFileDirectoryPath))
                return;

            if (LogLevel >= level)
            {
                DateTime LogStart = DateTime.Now;
                bool LogWritten = false;

                do
                {
                    try
                    {
                        FileInfo FI = null;

                        if (File.Exists(LogFileDirectoryPath + @"\" + logFile + ".txt"))
                        {
                            FI = new FileInfo(LogFileDirectoryPath + @"\" + logFile + ".txt");
                        }

                        if (FI != null && FI.Length > MaxLogSize * 1024 * 1024)
                        {
                            int NumberToKeep = LogFilesToKeep;

                            if (File.Exists(LogFileDirectoryPath + @"\" + logFile + "" + NumberToKeep.ToString() + ".txt"))
                            {
                                File.Delete(LogFileDirectoryPath + @"\" + logFile + "" + NumberToKeep.ToString() + ".txt");
                            }

                            for (int i = NumberToKeep - 1; i > 0; i--)
                            {
                                if (File.Exists(LogFileDirectoryPath + @"\" + logFile + "" + i.ToString() + ".txt"))
                                {
                                    File.Move(LogFileDirectoryPath + @"\" + logFile + "" + i.ToString() + ".txt", LogFileDirectoryPath + @"\" + logFile + "" + (i + 1).ToString() + ".txt");
                                }
                            }

                            File.Move(LogFileDirectoryPath + @"\" + logFile + ".txt", LogFileDirectoryPath + @"\" + logFile + "1.txt");
                        }
                        else
                        {
                            StreamWriter sw = new StreamWriter(LogFileDirectoryPath + @"\" + logFile + ".txt", true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + message);
                            sw.Close();
                        }

                        LogWritten = true;
                    }
                    catch
                    {

                    }

                }

                while (LogWritten == false && LogStart.AddSeconds(30) > DateTime.Now);
            }
        }
    }

}
