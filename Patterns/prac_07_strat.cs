using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public interface IPaymentStrategy
    {
        void Pay(int amount);
    }
    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(int amount)
        {
            Console.WriteLine($"Оплата {amount} кредитной картой.");
        }
    }
    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(int amount)
        {
            Console.WriteLine($"Оплата {amount} через PayPal.");
        }
    }
    public class BitcoinPayment : IPaymentStrategy
    {
        public void Pay(int amount)
        {
            Console.WriteLine($"Оплата {amount} через Bitcoin.");
        }
    }
    public class Order
    {
        private IPaymentStrategy _paymentStrategy;

        public Order(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void SetPaymentMethod(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void ProcessOrder(int amount)
        {
            Console.WriteLine("Обработка заказа...");
            _paymentStrategy.Pay(amount);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var order = new Order(new CreditCardPayment());
            order.ProcessOrder(100);

            order.SetPaymentMethod(new PayPalPayment());
            order.ProcessOrder(200);

            order.SetPaymentMethod(new BitcoinPayment());
            order.ProcessOrder(300);
        }
    }
}
