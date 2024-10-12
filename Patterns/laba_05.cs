using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public interface ITransport
    {
        void Move();
        void FuelUp();
    }
    public class Car : ITransport
    {
        public string Model { get; set; }
        public int Speed { get; set; }

        public Car(string model, int speed)
        {
            Model = model;
            Speed = speed;
        }

        public void Move()
        {
            Console.WriteLine($"Машина {Model} движется на скорости {Speed} km/h.");
        }

        public void FuelUp()
        {
            Console.WriteLine("Заправка автомобиля бензином.");
        }
    }
    public class Motorcycle : ITransport
    {
        public string Model { get; set; }
        public int Speed { get; set; }

        public Motorcycle(string model, int speed)
        {
            Model = model;
            Speed = speed;
        }

        public void Move()
        {
            Console.WriteLine($"Мотоцикл {Model} движется со скоростью {Speed} km/h.");
        }

        public void FuelUp()
        {
            Console.WriteLine("Заправка мотоцикла бензином.");
        }
    }
    public class Plane : ITransport
    {
        public string Model { get; set; }
        public int Speed { get; set; }

        public Plane(string model, int speed)
        {
            Model = model;
            Speed = speed;
        }

        public void Move()
        {
            Console.WriteLine($"Самолет {Model} летит на скорости {Speed} km/h.");
        }

        public void FuelUp()
        {
            Console.WriteLine("Заправка самолета авиационным топливом.");
        }
    }
    public class Bicycle : ITransport
    {
        public string Model { get; set; }
        public int Speed { get; set; }

        public Bicycle(string model, int speed)
        {
            Model = model;
            Speed = speed;
        }

        public void Move()
        {
            Console.WriteLine($"Велосипед {Model} двигается со скоростью {Speed} km/h.");
        }

        public void FuelUp()
        {
            Console.WriteLine("Для велосипеда не требуется топливо.");
        }
    }
    public abstract class TransportFactory
    {
        public abstract ITransport CreateTransport(string model, int speed);
    }
    public class CarFactory : TransportFactory
    {
        public override ITransport CreateTransport(string model, int speed)
        {
            return new Car(model, speed);
        }
    }
    public class MotorcycleFactory : TransportFactory
    {
        public override ITransport CreateTransport(string model, int speed)
        {
            return new Motorcycle(model, speed);
        }
    }
    public class PlaneFactory : TransportFactory
    {
        public override ITransport CreateTransport(string model, int speed)
        {
            return new Plane(model, speed);
        }
    }
    public class BicycleFactory : TransportFactory
    {
        public override ITransport CreateTransport(string model, int speed)
        {
            return new Bicycle(model, speed);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Выберите тип транспорта: 1 - Машина, 2 - Мотоцикл, 3 - Самолет, 4 - Велосипед");
            int choice = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();

            Console.WriteLine("Введите скорость:");
            int speed = int.Parse(Console.ReadLine());

            TransportFactory factory = null;

            switch (choice)
            {
                case 1:
                    factory = new CarFactory();
                    break;
                case 2:
                    factory = new MotorcycleFactory();
                    break;
                case 3:
                    factory = new PlaneFactory();
                    break;
                case 4:
                    factory = new BicycleFactory();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    return;
            }

            ITransport transport = factory.CreateTransport(model, speed);
            transport.Move();
            transport.FuelUp();
        }
    }

}
