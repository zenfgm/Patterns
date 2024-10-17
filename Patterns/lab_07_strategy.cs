using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public interface IShippingStrategy
    {
        decimal CalculateShippingCost(decimal weight, decimal distance);
    }

    public class StandardShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight, decimal distance)
        {
            return weight * 0.5m + distance * 0.1m;
        }
    }
    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight, decimal distance)
        {
            return (weight * 0.75m + distance * 0.2m) + 20;
        }
    }
    public class InternationalShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight, decimal distance)
        {
            return weight * 1.0m + distance * 0.5m + 30;
        }
    }
    public class DeliveryContext
    {
        private IShippingStrategy _shippingStrategy;

        public void SetShippingStrategy(IShippingStrategy strategy)
        {
            _shippingStrategy = strategy;
        }
        public decimal CalculateCost(decimal weight, decimal distance)
        {
            if (_shippingStrategy == null)
            {
                throw new InvalidOperationException("Стратегия доставки не установлена.");
            }
            return _shippingStrategy.CalculateShippingCost(weight, distance);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            DeliveryContext context = new DeliveryContext();
            Console.WriteLine("Тип доставки: 1 - Standart, 2 - Express, 3 - International");

            Console.WriteLine("------------------------");
            Console.Write("Выберите тип: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice <= 0 || choice >= 4)
            {
                Console.Write("Неверный тип доставки. Введите тип (1-3): ");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Вес посылки (кг): ");
            decimal weight = Convert.ToDecimal(Console.ReadLine());
            if (weight <= 0)
            {
                Console.Write("Вес должен быть больше 0: ");
                weight = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Расстояние (км): ");
            decimal distance = Convert.ToDecimal(Console.ReadLine());
            if (choice == 1)
            {
                context.SetShippingStrategy(new StandardShippingStrategy());
            }
            else if (choice == 2)
            {
                context.SetShippingStrategy(new ExpressShippingStrategy());
            }
            else if (choice == 3)
            {
                context.SetShippingStrategy(new InternationalShippingStrategy());
            }

            var data = context.CalculateCost(weight, distance);
            Console.WriteLine($"Стоимость доставки: {data:C}");

        }
    }
}
