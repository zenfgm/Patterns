using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class RoomBookingSystem
    {
        public void book(DateTime startDay, DateTime endDay, int people, string typeBooking)
        {
            Console.WriteLine($"Бронирование номера с {startDay.ToShortDateString()} по {endDay.ToShortDateString()} для {people} человек, тип бронирования: {typeBooking}.");
        }
    }
    public class CleaningService
    {
        public void cleanRoom(int number)
        {
            Console.WriteLine($"Уборка комнаты №{number} запланирована.");
        }
        public void notificationCleaning(int number, string message)
        {
            Console.WriteLine($"Уведомление для уборки комнаты №{number}: {message}");
        }
        public void checkOut(int number)
        {
            Console.WriteLine($"Выезд из комнаты №{number} выполнен, уборка начнется.");
        }
    }
    public class RestarauntSystem
    {
        public void bookTable(int people, DateTime time)
        {
            Console.WriteLine($"Забронирован стол для {people} человек на {time.ToShortTimeString()}.");
        }
        public void notificationKitchen(int people, int number)
        {
            Console.WriteLine($"Уведомление на кухню для заказа еды на {people} человек в комнате №{number}.");
        }
    }
    public class Notification
    {
        public void sendNotification(string message)
        {
            Console.WriteLine($"Уведомление отправлено: {message}");
        }
    }
    //Component
    public interface IOrganizationComponent
    {
        string Name { get; set; }
        double Salary { get; set; }
        int GetCount();
        double GetSum();
        void DisplayHierarchy(int indent = 0);
    }
    public class Employee : IOrganizationComponent
    {
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }

        public int GetCount() => 1;

        public double GetSum() => Salary;

        public void DisplayHierarchy(int indent = 0)
        {
            Console.WriteLine(new string('-', indent) + $" {Name} (Зарплата: {Salary})");
        }
    }
    public class Department : IOrganizationComponent
    {
        private List<IOrganizationComponent> _components = new List<IOrganizationComponent>();

        public string Name { get; set; }
        public double Salary { get; set; } = 0;

        public Department(string name)
        {
            Name = name;
        }

        public void AddComponent(IOrganizationComponent component)
        {
            _components.Add(component);
        }

        public int GetCount()
        {
            int count = 0;
            foreach (var component in _components)
            {
                count += component.GetCount();
            }
            return count;
        }

        public double GetSum()
        {
            double sum = 0;
            foreach (var component in _components)
            {
                sum += component.GetSum();
            }
            return sum;
        }

        public void DisplayHierarchy(int indent = 0)
        {
            Console.WriteLine(new string('-', indent) + $" {Name} (Общий бюджет: {GetSum()})");
            foreach (var component in _components)
            {
                component.DisplayHierarchy(indent + 2);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //FACADE
            DateTime startDay = new DateTime(2024, 12, 3);
            DateTime endDay = new DateTime(2024, 12, 10);
            HotelFacade hotelFacade = new HotelFacade();

            hotelFacade.BookRoomWithServices(startDay, endDay, 2, "Стандарт");
            hotelFacade.OrganizeEventWithRoomBooking(new DateTime(2024, 12, 15), 50, "Проектор, Звук");
            hotelFacade.BookRestaurantTableWithTaxi(4, new DateTime(2024, 12, 5, 19, 0, 0));
            Console.WriteLine();

            //Component
            Department headOffice = new Department("Головной офис");
            Department itDepartment = new Department("ИТ отдел");
            itDepartment.AddComponent(new Employee("Андрей", 120000));
            itDepartment.AddComponent(new Employee("Сергей", 110000));

            Department hrDepartment = new Department("Отдел кадров");
            hrDepartment.AddComponent(new Employee("Ольга", 90000));

            headOffice.AddComponent(itDepartment);
            headOffice.AddComponent(hrDepartment);

            headOffice.DisplayHierarchy();

            Console.WriteLine($"Общее количество сотрудников: {headOffice.GetCount()}");
            Console.WriteLine($"Общий бюджет: {headOffice.GetSum()}");
        }
    }
}
