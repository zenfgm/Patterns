using System;

public interface IVehicle
{
    void Drive();
    void Refuel();
}
public class Car : IVehicle
{
    private string _brand;
    private string _model;
    private string _fuelType;

    public Car(string brand, string model, string fuelType)
    {
        _brand = brand;
        _model = model;
        _fuelType = fuelType;
    }

    public void Drive()
    {
        Console.WriteLine($"Driving a {_brand} {_model} car.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Refueling the {_fuelType} car.");
    }
}
public class Motorcycle : IVehicle
{
    private string _type;
    private int _engineCapacity;

    public Motorcycle(string type, int engineCapacity)
    {
        _type = type;
        _engineCapacity = engineCapacity;
    }

    public void Drive()
    {
        Console.WriteLine($"Riding a {_type} motorcycle with {_engineCapacity}cc engine.");
    }

    public void Refuel()
    {
        Console.WriteLine("Refueling the motorcycle.");
    }
}
public class Truck : IVehicle
{
    private int _capacity;
    private int _numberOfAxles;

    public Truck(int capacity, int numberOfAxles)
    {
        _capacity = capacity;
        _numberOfAxles = numberOfAxles;
    }

    public void Drive()
    {
        Console.WriteLine($"Driving a truck with a {_capacity}kg capacity and {_numberOfAxles} axles.");
    }

    public void Refuel()
    {
        Console.WriteLine("Refueling the truck.");
    }
}
public abstract class VehicleFactory
{
    public abstract IVehicle CreateVehicle();
}
public class CarFactory : VehicleFactory
{
    private string _brand;
    private string _model;
    private string _fuelType;

    public CarFactory(string brand, string model, string fuelType)
    {
        _brand = brand;
        _model = model;
        _fuelType = fuelType;
    }

    public override IVehicle CreateVehicle()
    {
        return new Car(_brand, _model, _fuelType);
    }
}
public class MotorcycleFactory : VehicleFactory
{
    private string _type;
    private int _engineCapacity;

    public MotorcycleFactory(string type, int engineCapacity)
    {
        _type = type;
        _engineCapacity = engineCapacity;
    }

    public override IVehicle CreateVehicle()
    {
        return new Motorcycle(_type, _engineCapacity);
    }
}
public class TruckFactory : VehicleFactory
{
    private int _capacity;
    private int _numberOfAxles;

    public TruckFactory(int capacity, int numberOfAxles)
    {
        _capacity = capacity;
        _numberOfAxles = numberOfAxles;
    }

    public override IVehicle CreateVehicle()
    {
        return new Truck(_capacity, _numberOfAxles);
    }
}
public class Bus : IVehicle
{
    private int _seatingCapacity;

    public Bus(int seatingCapacity)
    {
        _seatingCapacity = seatingCapacity;
    }

    public void Drive()
    {
        Console.WriteLine($"Driving a bus with {_seatingCapacity} seats.");
    }

    public void Refuel()
    {
        Console.WriteLine("Refueling the bus.");
    }
}
public class BusFactory : VehicleFactory
{
    private int _seatingCapacity;

    public BusFactory(int seatingCapacity)
    {
        _seatingCapacity = seatingCapacity;
    }

    public override IVehicle CreateVehicle()
    {
        return new Bus(_seatingCapacity);
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Choose vehicle type: Car, Motorcycle, Truck, Bus");
        string vehicleType = Console.ReadLine();

        VehicleFactory factory = null;

        switch (vehicleType.ToLower())
        {
            case "car":
                Console.WriteLine("Enter brand:");
                string brand = Console.ReadLine();
                Console.WriteLine("Enter model:");
                string model = Console.ReadLine();
                Console.WriteLine("Enter fuel type:");
                string fuelType = Console.ReadLine();
                factory = new CarFactory(brand, model, fuelType);
                break;

            case "motorcycle":
                Console.WriteLine("Enter type (sport/touring):");
                string type = Console.ReadLine();
                Console.WriteLine("Enter engine capacity (cc):");
                int engineCapacity = int.Parse(Console.ReadLine());
                factory = new MotorcycleFactory(type, engineCapacity);
                break;

            case "truck":
                Console.WriteLine("Enter capacity (kg):");
                int capacity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter number of axles:");
                int numberOfAxles = int.Parse(Console.ReadLine());
                factory = new TruckFactory(capacity, numberOfAxles);
                break;

            case "bus":
                Console.WriteLine("Enter seating capacity:");
                int seatingCapacity = int.Parse(Console.ReadLine());
                factory = new BusFactory(seatingCapacity);
                break;

            default:
                Console.WriteLine("Invalid vehicle type.");
                return;
        }

        IVehicle vehicle = factory.CreateVehicle();
        vehicle.Drive();
        vehicle.Refuel();
    }
}

