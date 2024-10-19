using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public IPayment PaymentMethod { get; set; }
        public IDelivery DeliveryMethod { get; set; }
        public INotification NotificationMethod { get; set; }

        public Order()
        {
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public double CalculateTotal(DiscountCalculator discountCalculator)
        {
            double total = Items.Sum(item => item.Price * item.Quantity);
            return discountCalculator.CalculateDiscount(total);
        }

        public void ProcessOrder()
        {
            double total = CalculateTotal(new DiscountCalculator());
            PaymentMethod.ProcessPayment(total);
            DeliveryMethod.DeliverOrder(this);
            NotificationMethod.SendNotification("Ваш заказ обработан.");
        }
    }
    public interface IPayment
    {
        void ProcessPayment(double amount);
    }

    public class CreditCardPayment : IPayment
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Оплата {amount} обрабатывается с помощью кредитной карты.");
        }
    }

    public class PayPalPayment : IPayment
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Оплата {amount} обрабатывается через PayPal.");
        }
    }

    public class BankTransferPayment : IPayment
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Оплата {amount} обрабатывается банковским переводом.");
        }
    }
    public interface IDelivery
    {
        void DeliverOrder(Order order);
    }

    public class CourierDelivery : IDelivery
    {
        public void DeliverOrder(Order order)
        {
            Console.WriteLine("Заказ доставлен курьером.");
        }
    }

    public class PostDelivery : IDelivery
    {
        public void DeliverOrder(Order order)
        {
            Console.WriteLine("Заказ доставлен почтой.");
        }
    }

    public class PickUpPointDelivery : IDelivery
    {
        public void DeliverOrder(Order order)
        {
            Console.WriteLine("Заказ готов к выдаче в пункте самовывоза.");
        }
    }
    public interface INotification
    {
        void SendNotification(string message);
    }

    public class EmailNotification : INotification
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Письмо отправлено: {message}");
        }
    }

    public class SmsNotification : INotification
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"СМС отправлено: {message}");
        }
    }
    public class DiscountCalculator
    {
        public double CalculateDiscount(double total)
        {
            if (total > 100)
            {
                return total * 0.9;
            }
            return total;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();

            order.AddItem(new Item { Name = "Ноутбук", Price = 1000, Quantity = 1 });
            order.AddItem(new Item { Name = "Компьютерная мышь", Price = 50, Quantity = 2 });

            order.PaymentMethod = new CreditCardPayment();
            order.DeliveryMethod = new CourierDelivery();
            order.NotificationMethod = new EmailNotification();

            order.ProcessOrder();
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

}
