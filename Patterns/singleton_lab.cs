using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Laba_06
{
    using System;
    using System.IO;
    using System.Threading;

    public enum LogLevel
    {
        INFO,
        WARNING,
        ERROR
    }

    public sealed class Logger
    {
        private static Logger _instance = null;
        private static readonly object _lock = new object();
        private LogLevel _currentLogLevel = LogLevel.INFO;
        private string _logFilePath = "log.txt";

        private Logger() { }

        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }

        public void SetLogLevel(LogLevel level)
        {
            _currentLogLevel = level;
        }

        public void SetLogFilePath(string path)
        {
            _logFilePath = path;
        }

        public void Log(string message, LogLevel level)
        {
            if (level >= _currentLogLevel)
            {
                lock (_lock)
                {
                    using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                    {
                        writer.WriteLine($"{DateTime.Now}: {level} - {message}");
                    }
                }
            }
        }

        public string ReadLogs()
        {
            lock (_lock)
            {
                using (StreamReader reader = new StreamReader(_logFilePath))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Logger logger = Logger.GetInstance();
            logger.SetLogLevel(LogLevel.INFO);

            Thread thread1 = new Thread(() => LogMessages("Thread 1", LogLevel.INFO));
            Thread thread2 = new Thread(() => LogMessages("Thread 2", LogLevel.WARNING));
            Thread thread3 = new Thread(() => LogMessages("Thread 3", LogLevel.ERROR));

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine("Логи записываются в файл:");
            Console.WriteLine(logger.ReadLogs());
        }

        static void LogMessages(string threadName, LogLevel level)
        {
            Logger logger = Logger.GetInstance();

            for (int i = 0; i < 5; i++)
            {
                logger.Log($"{threadName} - Message {i}", level);
                Thread.Sleep(100);
            }
        }
    }

}
