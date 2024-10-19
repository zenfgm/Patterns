using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class Logger
    {
        public void Log(string level, string message)
        {
            Console.WriteLine($"{level.ToUpper()}: {message}");
        }
    }

    public class DatabaseService
    {
        public void Connect()
        {
            string connectionString = "Server=myServer;Database=myDb;User Id=myUser;Password=myPassword;";
        }
    }

    public class LoggingService
    {
        public void Log(string message)
        {
            string connectionString = "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
        }
    }

    public class Processor
    {
        public void ProcessNumbers(int[] numbers)
        {
            if (numbers?.Length > 0)
            {
                foreach (var number in numbers.Where(n => n > 0))
                {
                    Console.WriteLine(number);
                }
            }
        }

        public void PrintPositiveNumbers(int[] numbers)
        {
            var positiveNumbers = numbers.Where(n => n > 0).OrderBy(n => n).ToList();
            positiveNumbers.ForEach(Console.WriteLine);
        }

        public int Divide(int a, int b)
        {
            return b == 0 ? 0 : a / b;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public void SaveToDatabase()
        {
        }

        public void SendEmail()
        {
        }

        public void PrintAddressLabel()
        {
        }
    }

    public class FileReader
    {
        public string ReadFile(string filePath, bool useBuffer = true, int bufferSize = 1024)
        {
            return "file content";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program has started.");
        }
    }
}
