using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversitySystem
{
    class University
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public University(string name, string phoneNumber, string emailAddress)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }
    }

    class Faculty
    {
        public string Name { get; set; }
        public List<Department> Departments { get; set; } = new();

        public bool AddDepartment(Department department)
        {
            Departments.Add(department);
            return true;
        }

        public bool RemoveDepartment(Department department)
        {
            return Departments.Remove(department);
        }
    }
    class Department
    {
        public string Name { get; set; }
        public List<Professor> Professors { get; set; } = new();
        public List<Course> Courses { get; set; } = new();

        public bool AddProfessor(Professor professor)
        {
            Professors.Add(professor);
            return true;
        }

        public bool RemoveProfessor(Professor professor)
        {
            return Professors.Remove(professor);
        }
    }

    class Professor
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public List<Course> Courses { get; set; } = new();
    }

    class Course
    {
        public string Name { get; set; }
        public Department Department { get; set; }
        public List<Student> Students { get; set; } = new();
        public List<Schedule> Schedules { get; set; } = new();
    }

    class Student : IStudent
    {
        public int IdStudent { get; set; }
        public List<Course> Courses { get; set; } = new();
    }

    interface IStudent
    {
    }

    class Schedule
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Classroom { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var university = new University("Tech University", "123-456-789", "info@techuniversity.com");

            var faculty = new Faculty { Name = "Engineering" };
            var department = new Department { Name = "Computer Science" };

            faculty.AddDepartment(department);

            var professor = new Professor { Name = "Dr. Smith", Position = "Head of Department" };
            department.AddProfessor(professor);

            var course = new Course { Name = "Programming 101", Department = department };
            department.Courses.Add(course);

            var student = new Student { IdStudent = 1 };
            student.Courses.Add(course);
            course.Students.Add(student);

            var schedule = new Schedule { Date = DateTime.Now, Time = "10:00 AM", Classroom = "Room 101" };
            course.Schedules.Add(schedule);

            Console.WriteLine($"{university.Name} offers {faculty.Name} faculty with department {department.Name}.");
        }
    }
}
