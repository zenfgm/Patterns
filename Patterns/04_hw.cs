using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    //1
    public class Order
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class PriceCalculator
    {
        public double CalculateTotalPrice(Order order)
        {
            return order.Quantity * order.Price * 0.9;
        }
    }

    public class PaymentProcessor
    {
        public void ProcessPayment(string paymentDetails)
        {
            // Логика обработки платежа
            Console.WriteLine("Payment processed using: " + paymentDetails);
        }
    }

    public class NotificationService
    {
        public void SendConfirmationEmail(string email)
        {
            // Логика отправки уведомления
            Console.WriteLine("Confirmation email sent to: " + email);
        }
    }
    //2
    public class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }
    }

    public interface ISalaryCalc
    {
        double CalcSalary(Employee employee);
    }

    public class PermanentEmployeeSalaryCalculator : ISalaryCalc
    {
        public double CalcSalary(Employee employee)
        {
            return employee.BaseSalary * 1.2;
        }
    }

    public class ContractEmployeeSalaryCalculator : ISalaryCalc
    {
        public double CalcSalary(Employee employee)
        {
            return employee.BaseSalary * 1.1;
        }
    }

    public class InternSalaryCalculator : ISalaryCalc
    {
        public double CalcSalary(Employee employee)
        {
            return employee.BaseSalary * 0.8;
        }
    }
    //3
    public interface IPrinter
    {
        void Print(string content);
    }

    public interface IScanner
    {
        void Scan(string content);
    }

    public interface IFax
    {
        void Fax(string content);
    }

    public class AllInOnePrinter : IPrinter, IScanner, IFax
    {
        public void Print(string content)
        {
            Console.WriteLine("Printing: " + content);
        }

        public void Scan(string content)
        {
            Console.WriteLine("Scanning: " + content);
        }

        public void Fax(string content)
        {
            Console.WriteLine("Faxing: " + content);
        }
    }

    public class BasicPrinter : IPrinter
    {
        public void Print(string content)
        {
            Console.WriteLine("Printing: " + content);
        }
    }
    //4
    public interface IMessageSender
    {
        void SendMessage(string message);
    }

    public class EmailSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine("Email sent: " + message);
        }
    }

    public class SmsSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine("SMS sent: " + message);
        }
    }

    public class Notification
    {
        private readonly IMessageSender _messageSender;

        public Notification(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public void SendNotif(string message)
        {
            _messageSender.SendMessage(message);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //1
            Console.WriteLine("//1");

            Order order = new Order { ProductName = "Laptop", Quantity = 2, Price = 1000 };
            PriceCalculator calculator = new PriceCalculator();
            PaymentProcessor paymentProcessor = new PaymentProcessor();
            NotificationService notificationService = new NotificationService();

            double totalPrice = calculator.CalculateTotalPrice(order);
            paymentProcessor.ProcessPayment("Credit Card");
            notificationService.SendConfirmationEmail("customer@example.com");

            //2
            Console.WriteLine("//2");

            Employee permanentEmployee = new Employee { Name = "John", BaseSalary = 1000 };
            ISalaryCalc calc = new PermanentEmployeeSalaryCalculator();
            double salary = calc.CalcSalary(permanentEmployee);
            Console.WriteLine("Salary: " + salary);

            //3
            Console.WriteLine("//3");

            IPrinter printer = new BasicPrinter();
            printer.Print("Document");

            //4
            Console.WriteLine("//4");

            IMessageSender emailSender = new EmailSender();
            Notification notification = new Notification(emailSender);
            notification.SendNotif("Your order has been shipped.");
        }
    }
}
