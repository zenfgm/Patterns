using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    abstract class Vehicle
    {
        public string Brand { get; }
        public string Model { get; }
        public int Year { get; }

        public Vehicle(string brand, string model, int year)
        {
            Brand = brand;
            Model = model;
            Year = year;
        }

        public virtual void StartEngine()
        {
            Console.WriteLine($"Двигатель {Brand} {Model} запущен.");
        }

        public virtual void StopEngine()
        {
            Console.WriteLine($"Двигатель {Brand} {Model} остановлен.");
        }
    }
    class Car : Vehicle
    {
        public int NumberOfDoors { get; }
        public string TransmissionType { get; }

        public Car(string brand, string model, int year, int numberOfDoors, string transmissionType)
            : base(brand, model, year)
        {
            NumberOfDoors = numberOfDoors;
            TransmissionType = transmissionType;
        }

        public override void StartEngine()
        {
            Console.WriteLine($"Двигатель автомобиля {Brand} {Model} с {NumberOfDoors} дверьми запущен.");
        }
    }
    class Motorcycle : Vehicle
    {
        public string BodyType { get; }
        public bool HasBox { get; }

        public Motorcycle(string brand, string model, int year, string bodyType, bool hasBox)
            : base(brand, model, year)
        {
            BodyType = bodyType;
            HasBox = hasBox;
        }

        public override void StartEngine()
        {
            Console.WriteLine($"Двигатель мотоцикла {Brand} {Model} типа {BodyType} запущен.");
        }
    }
    class Garage
    {
        private List<Vehicle> vehicles = new List<Vehicle>();

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            Console.WriteLine($"Транспортное средство {vehicle.Brand} {vehicle.Model} добавлено в гараж.");
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
            Console.WriteLine($"Транспортное средство {vehicle.Brand} {vehicle.Model} удалено из гаража.");
        }

        public void ListVehicles()
        {
            Console.WriteLine("Транспортные средства в гараже:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"- {vehicle.Brand} {vehicle.Model} ({vehicle.Year})");
            }
        }
    }
    class Fleet
    {
        private List<Garage> garages = new List<Garage>();

        public void AddGarage(Garage garage)
        {
            garages.Add(garage);
            Console.WriteLine("Гараж добавлен в автопарк.");
        }

        public void RemoveGarage(Garage garage)
        {
            garages.Remove(garage);
            Console.WriteLine("Гараж удален из автопарка.");
        }

        public void ListGarages()
        {
            Console.WriteLine("Гаражи в автопарке:");
            foreach (var garage in garages)
            {
                garage.ListVehicles();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Car car1 = new Car("Toyota", "Camry", 2020, 4, "Автоматическая");
            Car car2 = new Car("BMW", "X5", 2022, 4, "Механическая");
            Motorcycle motorcycle1 = new Motorcycle("Harley Davidson", "Sportster", 2019, "Круизер", true);

            Garage garage1 = new Garage();
            garage1.AddVehicle(car1);
            garage1.AddVehicle(motorcycle1);

            Garage garage2 = new Garage();
            garage2.AddVehicle(car2);

            Fleet fleet = new Fleet();
            fleet.AddGarage(garage1);
            fleet.AddGarage(garage2);

            fleet.ListGarages();

            garage1.RemoveVehicle(car1);
            fleet.RemoveGarage(garage2);

            fleet.ListGarages();
        }
    }
}
