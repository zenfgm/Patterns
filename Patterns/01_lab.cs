using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    using System;
    using System.Collections.Generic;
    abstract class Employee
    {
        public string Name { get; }
        public int EmployeeId { get; }
        public string Position { get; }

        public Employee(string name, int employeeId, string position)
        {
            Name = name;
            EmployeeId = employeeId;
            Position = position;
        }

        public abstract double CalculateSalary();
    }
    class Worker : Employee
    {
        public double HourlyRate { get; }
        public int HoursWorked { get; }

        public Worker(string name, int employeeId, double hourlyRate, int hoursWorked)
            : base(name, employeeId, "Рабочий")
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public override double CalculateSalary()
        {
            return HourlyRate * HoursWorked;
        }
    }
    class Manager : Employee
    {
        public double FixedSalary { get; }
        public double Bonus { get; }

        public Manager(string name, int employeeId, double fixedSalary, double bonus)
            : base(name, employeeId, "Менеджер")
        {
            FixedSalary = fixedSalary;
            Bonus = bonus;
        }

        public override double CalculateSalary()
        {
            return FixedSalary + Bonus;
        }
    }
    class EmployeeSystem
    {
        private List<Employee> employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
            Console.WriteLine($"Сотрудник {employee.Name} добавлен в систему.");
        }

        public void ShowSalaries()
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Name} ({employee.Position}): Зарплата = {employee.CalculateSalary()}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            EmployeeSystem employeeSystem = new EmployeeSystem();

            Worker worker1 = new Worker("Иван Иванов", 1, 15.5, 160);
            Worker worker2 = new Worker("Петр Петров", 2, 18.0, 150);

            Manager manager1 = new Manager("Ольга Сидорова", 3, 50000, 10000);
            Manager manager2 = new Manager("Анна Кузнецова", 4, 60000, 15000);

            employeeSystem.AddEmployee(worker1);
            employeeSystem.AddEmployee(worker2);
            employeeSystem.AddEmployee(manager1);
            employeeSystem.AddEmployee(manager2);

            employeeSystem.ShowSalaries();
        }
    }

}
