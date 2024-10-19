using Patterns;
using Patterns.HW_06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    //1
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public double TaxRate { get; set; }
    }

    public class InvoiceCalculator
    {
        public double CalculateTotal(Invoice invoice)
        {
            double subTotal = invoice.Items.Sum(item => item.Price);
            return subTotal + (subTotal * invoice.TaxRate);
        }
    }

    public class InvoiceRepository
    {
        public void SaveToDatabase(Invoice invoice)
        {
            // Логика сохранения счета в базу данных
            Console.WriteLine($"Invoice {invoice.Id} saved to database.");
        }
    }
    //2
    public abstract class DiscountCalculator
    {
        public abstract double CalculateDiscount(double amount);
    }

    public class RegularCustomerDiscount : DiscountCalculator
    {
        public override double CalculateDiscount(double amount)
        {
            return amount; // Без скидки
        }
    }

    public class SilverCustomerDiscount : DiscountCalculator
    {
        public override double CalculateDiscount(double amount)
        {
            return amount * 0.9; // 10% скидка
        }
    }

    public class GoldCustomerDiscount : DiscountCalculator
    {
        public override double CalculateDiscount(double amount)
        {
            return amount * 0.8; // 20% скидка
        }
    }
    //3
    public interface IWorkable
    {
        void Work();
    }

    public interface IEatable
    {
        void Eat();
    }

    public interface ISleepable
    {
        void Sleep();
    }

    public class HumanWorker : IWorkable, IEatable, ISleepable
    {
        public void Work()
        {
            Console.WriteLine("Human is working.");
        }

        public void Eat()
        {
            Console.WriteLine("Human is eating.");
        }

        public void Sleep()
        {
            Console.WriteLine("Human is sleeping.");
        }
    }

    public class RobotWorker : IWorkable
    {
        public void Work()
        {
            Console.WriteLine("Robot is working.");
        }
    }
    //4
    public interface INotificationSender
    {
        void Send(string message);
    }

    public class EmailService : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending email: {message}");
        }
    }

    public class SmsService : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending SMS: {message}");
        }
    }

    public class Notification
    {
        private readonly INotificationSender _notificationSender;

        public Notification(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender;
        }

        public void Send(string message)
        {
            _notificationSender.Send(message);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //1
            Console.WriteLine("//1");

            var invoice = new Invoice { Id = 1, TaxRate = 0.1 };
            invoice.Items.Add(new Item { Name = "Item1", Price = 100 });
            invoice.Items.Add(new Item { Name = "Item2", Price = 200 });

            var calculator = new InvoiceCalculator();
            double total = calculator.CalculateTotal(invoice);

            var repository = new InvoiceRepository();
            repository.SaveToDatabase(invoice);

            Console.WriteLine($"Total: {total}");

            //2
            Console.WriteLine("//2");

            double amount = 1000;
            DiscountCalculator discountCalculator = new GoldCustomerDiscount();
            double discountedAmount = discountCalculator.CalculateDiscount(amount);
            Console.WriteLine($"Discounted amount: {discountedAmount}");

            //3
            Console.WriteLine("//3");

            IWorkable humanWorker = new HumanWorker();
            humanWorker.Work();

            IWorkable robotWorker = new RobotWorker();
            robotWorker.Work();

            //4
            Console.WriteLine("//4");

            INotificationSender emailService = new EmailService();
            Notification notification = new Notification(emailService);
            notification.Send("Hello via Email!");

            INotificationSender smsService = new SmsService();
            notification = new Notification(smsService);
            notification.Send("Hello via SMS!");
        }
    }

}
