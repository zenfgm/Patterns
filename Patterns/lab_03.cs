using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class OrderService
    {
        public void CreateOrder(string productName, int quantity, double price)
        {
            double totalPrice = quantity * price;
            Console.WriteLine($"Order for {productName} created. Total: {totalPrice}");
        }
        public void UpdateOrder(string productName, int quantity, double price)
        {
            double totalPrice = quantity * price;
            Console.WriteLine($"Order for {productName} updated. New total: {totalPrice}");
        }
    }

    public class Car
    {
        public void Start()
        {
            Console.WriteLine("Car is starting");
        }
        public void Stop()
        {
            Console.WriteLine("Car is stopping");
        }
    }

    public class Truck
    {
        public void Start()
        {
            Console.WriteLine("Truck is starting");
        }
        public void Stop()
        {
            Console.WriteLine("Truck is stopping");
        }
    }

    public interface IOperation
    {
        void Execute();
    }

    public class AdditionOperation : IOperation
    {
        private int _a;
        private int _b;

        public AdditionOperation(int a, int b)
        {
            _a = a;
            _b = b;
        }

        public void Execute()
        {
            Console.WriteLine(_a + _b);
        }
    }

    public class Calculator
    {
        public void PerformOperation(IOperation operation)
        {
            operation.Execute();
        }
    }

    public class Singleton
    {
        private static Singleton _instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }

        public void DoSomething()
        {
            Console.WriteLine("Doing something...");
        }
    }

    public class Client
    {
        public void Execute()
        {
            Singleton.Instance.DoSomething();
        }
    }

    public interface IShape
    {
        double CalculateArea();
    }

    public class Circle : IShape
    {
        private double _radius;

        public Circle(double radius)
        {
            _radius = radius;
        }

        public double CalculateArea()
        {
            return Math.PI * _radius * _radius;
        }
    }

    public class Square : IShape
    {
        private double _side;

        public Square(double side)
        {
            _side = side;
        }

        public double CalculateArea()
        {
            return _side * _side;
        }
    }

    public class MathOperations
    {
        public int Add(int a, int b, bool shouldLog = false)
        {
            int result = a + b;
            if (shouldLog)
            {
                Console.WriteLine($"Result: {result}");
            }
            return result;
        }
    }
}
