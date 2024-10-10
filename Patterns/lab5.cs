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
        Console.WriteLine($"Вождение автомобиля {_brand} {_model}.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Заправка автомобиля {_fuelType} топливом.");
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
        Console.WriteLine($"Езда на мотоцикле {_type} с двигателем {_engineCapacity} куб.см.");
    }

    public void Refuel()
    {
        Console.WriteLine("Заправка мотоцикла.");
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
        Console.WriteLine($"Вождение грузовика грузоподъемностью {_capacity} кг и {_numberOfAxles} осями.");
    }

    public void Refuel()
    {
        Console.WriteLine("Заправка грузовика.");
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
        Console.WriteLine($"Вождение автобуса на {_seatingCapacity} места");
    }

    public void Refuel()
    {
        Console.WriteLine("Заправка автобуса.");
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
        Console.WriteLine("Выберите тип транспортного средства: Автомобиль(1), Мотоцикл(2), Грузовик(3), Автобус(4).");
        string vehicleType = Console.ReadLine();

        VehicleFactory factory = null;

        switch (vehicleType.ToLower())
        {
            case "1":
                Console.WriteLine("Введите бренд:");
                string brand = Console.ReadLine();
                Console.WriteLine("Введите модель:");
                string model = Console.ReadLine();
                Console.WriteLine("Введите тип топлива:");
                string fuelType = Console.ReadLine();
                factory = new CarFactory(brand, model, fuelType);
                break;

            case "2":
                Console.WriteLine("Введите тип (sport/touring):");
                string type = Console.ReadLine();
                Console.WriteLine("Введите объем двигателя (куб.см):");
                int engineCapacity = int.Parse(Console.ReadLine());
                factory = new MotorcycleFactory(type, engineCapacity);
                break;

            case "3":
                Console.WriteLine("Введите грузоподъемность (кг):");
                int capacity = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите количество осей:");
                int numberOfAxles = int.Parse(Console.ReadLine());
                factory = new TruckFactory(capacity, numberOfAxles);
                break;

            case "4":
                Console.WriteLine("Введите кол-во сидячих мест:");
                int seatingCapacity = int.Parse(Console.ReadLine());
                factory = new BusFactory(seatingCapacity);
                break;

            default:
                Console.WriteLine("Неправильный вид т/c.");
                return;
        }

        IVehicle vehicle = factory.CreateVehicle();
        vehicle.Drive();
        vehicle.Refuel();
    }
}

