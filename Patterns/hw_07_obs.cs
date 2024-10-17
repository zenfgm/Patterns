using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public interface IObserver
    {
        void Update(string currency, decimal rate);
    }
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
    public class CurrencyExchange : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private Dictionary<string, decimal> _currencyRates = new Dictionary<string, decimal>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                foreach (var currency in _currencyRates)
                {
                    observer.Update(currency.Key, currency.Value);
                }
            }
        }

        public void SetCurrencyRate(string currency, decimal rate)
        {
            _currencyRates[currency] = rate;
            Notify();
        }
    }
    public class ConsoleObserver : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"ConsoleObserver: Курс {currency} обновлен до {rate}");
        }
    }
    public class LogObserver : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            System.IO.File.AppendAllText("log.txt", $"LogObserver: Курс {currency} обновлен до {rate}\n");
        }
    }
    public class EmailObserver : IObserver
    {
        private string _email;

        public EmailObserver(string email)
        {
            _email = email;
        }

        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"EmailObserver: Отправка письма на {_email} — курс {currency} обновлен до {rate}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CurrencyExchange currencyExchange = new CurrencyExchange();

            ConsoleObserver consoleObserver = new ConsoleObserver();
            LogObserver logObserver = new LogObserver();
            EmailObserver emailObserver = new EmailObserver("user@mail.com");

            currencyExchange.Attach(consoleObserver);
            currencyExchange.Attach(logObserver);
            currencyExchange.Attach(emailObserver);

            currencyExchange.SetCurrencyRate("USD", 74.5m);
            currencyExchange.SetCurrencyRate("EUR", 86.7m);

            currencyExchange.Detach(consoleObserver);

            currencyExchange.SetCurrencyRate("GBP", 102.3m);
        }
    }
}
