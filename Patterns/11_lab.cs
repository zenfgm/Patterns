using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem
{
    class Book
    {
        public List<string> Books { get; set; } = new();

        public string AddBook(string book)
        {
            Books.Add(book);
            return $"{book} added successfully.";
        }

        public string RemoveBook(string book)
        {
            if (Books.Remove(book))
                return $"{book} removed successfully.";
            return $"{book} not found.";
        }
    }

    class Delivery
    {
        public string Type { get; set; }

        public bool ShipDelivery(string delivery)
        {
            Console.WriteLine($"Delivery {delivery} shipped successfully.");
            return true;
        }
    }

    class Payment
    {
        public int Price { get; set; }

        public bool CreatePayment(int payment)
        {
            Price = payment;
            Console.WriteLine($"Payment of {payment} created successfully.");
            return true;
        }
    }

    class Notification
    {
        public string Notify { get; set; }

        public string SendNotification(string notify)
        {
            Notify = notify;
            Console.WriteLine($"Notification sent: {notify}");
            return Notify;
        }
    }

    class Order
    {
        public int Id { get; set; }
        public string Book { get; set; }
        public DateTime Date { get; set; }

        public bool CreateOrder(Order order)
        {
            Console.WriteLine($"Order {order.Id} for {order.Book} created on {order.Date}.");
            return true;
        }

        public int RemoveOrder(int id)
        {
            Console.WriteLine($"Order {id} removed.");
            return id;
        }
    }

    class User
    {
        public string Name { get; set; }
        public List<string> BookList { get; set; } = new();

        public bool CreateOrder(Order order)
        {
            Console.WriteLine($"User {Name} created order {order.Id}.");
            return true;
        }
    }

    interface IOrder
    {
        string Name { get; set; }
        void ProcessOrder();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bookManager = new Book();
            bookManager.AddBook("The Great Gatsby");
            bookManager.AddBook("1984");

            var user = new User { Name = "Alice" };
            var order = new Order { Id = 1, Book = "The Great Gatsby", Date = DateTime.Now };

            user.CreateOrder(order);

            var payment = new Payment();
            payment.CreatePayment(500);

            var delivery = new Delivery { Type = "Express" };
            delivery.ShipDelivery("Order 1");

            var notification = new Notification();
            notification.SendNotification("Your order has been shipped!");
        }
    }
}
